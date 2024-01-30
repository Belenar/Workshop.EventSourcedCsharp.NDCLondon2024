namespace BeerSender.Domain;

abstract class Command_handler<TCommand, TAggregate>
    where TAggregate : Aggregate, new()
    where TCommand : Command
{
    private readonly IEnumerable<Event> _event_stream;
    private readonly Action<Event> _publish_event;

    protected Command_handler(IEnumerable<Event> event_stream,
        Action<Event> publish_event)
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

    protected abstract IEnumerable<Event> Handle_command(
        TAggregate aggregate,
        TCommand command);
}