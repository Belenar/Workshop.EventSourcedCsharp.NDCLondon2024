using BeerSender.Domain;
using BeerSender.Domain.Boxes;
using BeerSender.Web.Read_database;

namespace BeerSender.Web.Projections;

public class Box_status_projection : Projection
{
    private readonly Read_context _db_context;

    public int Batch_size => 500;
    public TimeSpan Wait_time => TimeSpan.FromMilliseconds(5000);

    public Type[] Event_types => new[]
    {
        typeof(Box_created),
        typeof(Beer_added),
        typeof(Box_closed),
        typeof(Box_shipped)
    };

    public Box_status_projection(Read_context db_context)
    {
        _db_context = db_context;
    }

    public void Handle_batch(IEnumerable<Event_message> batch)
    {
        foreach (var event_message in batch)
        {
            Handle_message(event_message);
        }

        _db_context.SaveChanges();
    }

    private void Handle_message(Event_message event_message)
    {
        switch (event_message.Event)
        {
            case Box_created:
                _db_context.Box_statuses.Add(new Box_status
                {
                    Aggregate_id = event_message.Aggregate_id,
                    Number_of_bottles = 0,
                    Shipment_status = Shipment_status.Open
                });
                break;
            case Beer_added:
                {
                    var record = _db_context.Box_statuses.Find(event_message.Aggregate_id);
                    record!.Number_of_bottles++;
                    break;
                }
            case Box_closed:
                {
                    var record = _db_context.Box_statuses.Find(event_message.Aggregate_id);
                    record!.Shipment_status = Shipment_status.Closed;
                    break;
                }
            case Box_shipped:
            {
                var record = _db_context.Box_statuses.Find(event_message.Aggregate_id);
                record!.Shipment_status = Shipment_status.Shipped;
                break;
            }
        }
    }
}