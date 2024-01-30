namespace BeerSender.Domain;

public interface Event { }

public record Event_message(
    Guid Aggregate_id,
    int Sequence,
    Event Event);