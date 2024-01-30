using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeerSender.Web.Event_stream;

public class Event_context : DbContext
{
    public DbSet<Event_model> Events => Set<Event_model>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Event_model_mapping());
    }
}

public class Event_model
{
    public Guid Aggregate_id { get; set; }
    public int Sequence_nr { get; set; }
    public string Event_type { get; set; }
    public string Event_Payload { get; set; }
    public DateTime Timestamp { get; set; }
    public byte[] Row_version { get; set; }
    // Possibly: correlation ID, conversation ID

    private object? _event;

    public object Event
    {
        get
        {
            if (_event is null)
            {

            }
            return _event;
        }
        set
        {

        }
    }
}

class Event_model_mapping : IEntityTypeConfiguration<Event_model>
{
    public void Configure(EntityTypeBuilder<Event_model> builder)
    {
        builder.HasKey(e => new { e.Aggregate_id, e.Sequence_nr });

        builder.Property(e => e.Row_version).IsRowVersion();
    }
}