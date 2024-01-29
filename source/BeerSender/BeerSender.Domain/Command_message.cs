namespace BeerSender.Domain;

public record Command_message(
    Guid Aggregate_id,
    object Command);