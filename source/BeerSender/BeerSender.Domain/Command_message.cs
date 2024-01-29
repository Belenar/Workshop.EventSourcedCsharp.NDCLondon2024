namespace BeerSender.Domain;

public interface Command { }

public record Command_message(
    Guid Aggregate_id,
    Command Command);