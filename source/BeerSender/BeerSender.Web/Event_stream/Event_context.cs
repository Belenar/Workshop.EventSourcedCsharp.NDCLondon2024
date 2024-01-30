using System.Text.Json;
using BeerSender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeerSender.Web.Event_stream;

public class Event_context : DbContext
{
    public Event_context(DbContextOptions<Event_context> options) : base(options)
    { }

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
    public string Event_payload { get; set; }
    public DateTime Timestamp { get; set; }
    public ulong Row_version { get; set; }
    // Possibly: correlation ID, conversation ID

    private Event? _event;

    public Event Event
    {
        get
        {
            if (_event is null)
            {
                var type = TypeLookup[Event_type];
                _event = JsonSerializer.Deserialize(Event_payload, type) as Event;
            }
            return _event!;
        }
        set
        {
            _event = value;
            Event_type = _event.GetType().Name;
            Event_payload = JsonSerializer.Serialize((object)_event);
        }
    }

    #region Type dictionary
    private static readonly Dictionary<string, Type> TypeLookup = new();

    static Event_model()
    {
        var event_types = typeof(Event)
            .Assembly
            .GetTypes()
            .Where(type => !type.IsAbstract && typeof(Event).IsAssignableFrom(type));

        foreach (var event_type in event_types)
        {
            TypeLookup[event_type.Name] = event_type;
        }
    }

    #endregion
}

class Event_model_mapping : IEntityTypeConfiguration<Event_model>
{
    public void Configure(EntityTypeBuilder<Event_model> builder)
    {
        builder.HasKey(e => new { e.Aggregate_id, e.Sequence_nr });

        builder.Ignore(e => e.Event);

        builder.Property(e => e.Event_type)
            .HasMaxLength(256)
            .HasColumnType("varchar");

        builder.Property(e => e.Event_payload)
            .HasMaxLength(2048)
            .HasColumnType("varchar");

        builder.Property(e => e.Row_version).IsRowVersion();
    }
}