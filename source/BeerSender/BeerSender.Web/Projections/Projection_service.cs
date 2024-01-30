using BeerSender.Domain;
using BeerSender.Web.Event_stream;
using BeerSender.Web.Read_database;
using Microsoft.EntityFrameworkCore;

namespace BeerSender.Web.Projections;

public class Projection_service<TProjection> : BackgroundService
    where TProjection : class, Projection
{
    private readonly IServiceProvider _service_provider;

    public Projection_service(IServiceProvider service_provider)
    {
        _service_provider = service_provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var checkpoint = await Get_checkpoint();

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _service_provider.CreateScope();
            var event_context = scope.ServiceProvider.GetRequiredService<Event_context>();
            var read_context = scope.ServiceProvider.GetRequiredService<Read_context>();

            var transaction = await read_context.Database.BeginTransactionAsync(stoppingToken);

            var projection = scope.ServiceProvider.GetRequiredService<TProjection>();

            var events = await Get_batch(checkpoint, projection, event_context);

            if (events.Any())
            {
                projection.Handle_batch(events.Select(e => new Event_message(
                    e.Aggregate_id,
                    e.Sequence_nr,
                    e.Event)));

                checkpoint = events.Last().Row_version;
                await Write_checkpoint(read_context, checkpoint);
            }
            else
            {
                await Task.Delay(projection.Wait_time);
            }
            await transaction.CommitAsync(stoppingToken);
        }
    }

    private async Task Write_checkpoint(Read_context read_context, ulong checkpoint)
    {
        var checkpoint_record = await read_context.Projection_checkpoints
            .FindAsync(typeof(TProjection).Name);
        checkpoint_record!.Event_version = checkpoint;
        await read_context.SaveChangesAsync();
    }

    private async Task<ulong> Get_checkpoint()
    {
        using var scope = _service_provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<Read_context>();

        var checkpoint = await context.Projection_checkpoints.FindAsync(
            typeof(TProjection).Name);

        if (checkpoint == null)
        {
            checkpoint = new Projection_checkpoint
            {
                Projection_name = typeof(TProjection).Name,
                Event_version = 0
            };
            context.Projection_checkpoints.Add(checkpoint);
            await context.SaveChangesAsync();
        }

        return checkpoint.Event_version;
    }

    private async Task<IList<Event_model>> Get_batch(
        ulong checkpoint, 
        TProjection projection, 
        Event_context event_context)
    {
        var type_list = projection.Event_types.Select(t => t.Name).ToList();

        var batch = await event_context.Events
            .Where(e => type_list.Contains(e.Event_type))
            .Where(e => e.Row_version > checkpoint)
            .OrderBy(e => e.Row_version)
            .Take(projection.Batch_size)
            .ToListAsync();

        return batch;
    }
}