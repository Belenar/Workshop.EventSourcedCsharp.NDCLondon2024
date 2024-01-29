namespace BeerSender.Domain.Boxes.Handlers;

public class Get_box_handler
{
    private readonly IEnumerable<object> _event_stream;
    private readonly Action<object> _publish_event;

    public Get_box_handler(IEnumerable<object> event_stream,
        Action<object> publish_event)
    {
        _event_stream = event_stream;
        _publish_event = publish_event;
    }

    public void Handle(Get_box command)
    {
        Box aggregate = new Box();

        foreach (var @event in _event_stream)
        {
            aggregate.Apply(@event);
        }

        var new_events = HandleInternal(aggregate, command);

        foreach (var new_event in new_events)
        {
            _publish_event(new_event);
        }
    }

    private IEnumerable<object> HandleInternal(
        Box aggregate,
        Get_box command)
    {
        var capacity = Capacity.Create(command.Desired_number_of_spots);
        yield return new Box_created(capacity);
    }
}