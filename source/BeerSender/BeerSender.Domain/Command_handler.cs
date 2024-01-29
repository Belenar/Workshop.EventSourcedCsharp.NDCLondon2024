namespace BeerSender.Domain;

abstract class Command_handler<TCommand, TAggregate>
    where TAggregate : Aggregate, new()
    where TCommand : Command
{
    private readonly IEnumerable<object> _event_stream;
    private readonly Action<object> _publish_event;

    protected Command_handler(IEnumerable<object> event_stream,
        Action<object> publish_event)
    {
        _event_stream = event_stream;
        _publish_event = publish_event;
    }

    public void Handle(TCommand command)
    {
        TAggregate aggregate = new();

        foreach (var @event in _event_stream)
        {
            aggregate.Apply((dynamic)@event);
        }

        var new_events = Handle_command(aggregate, command);

        foreach (var new_event in new_events)
        {
            _publish_event(new_event);
        }
    }

    protected abstract IEnumerable<object> Handle_command(
        TAggregate aggregate,
        TCommand command);
}