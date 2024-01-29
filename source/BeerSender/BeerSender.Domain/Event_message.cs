namespace BeerSender.Domain;

public record Event_message(
    Guid Aggregate_id,
    int Sequence,
    object Event);