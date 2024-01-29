using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Handlers;

namespace BeerSender.Domain;

public record Command_message(
    Guid Aggregate_id,
    object Command);

public record Event_message(
    Guid Aggregate_id,
    int Sequence,
    object Event);

public class Command_router
{
    private readonly Func<Guid, IEnumerable<Event_message>> _event_stream;
    private readonly Action<Guid, int, object> _publish_event;

    public Command_router(
        Func<Guid, IEnumerable<Event_message>> event_stream,
        Action<Guid, int, object> publish_event)
    {
        _event_stream = event_stream;
        _publish_event = publish_event;
    }

    public void Handle(Command_message command)
    {
        var filtered_stream = _event_stream(command.Aggregate_id);
        var max_sequence = filtered_stream.Max(e => e.Sequence);
        Action<object> publish = (ev => _publish_event(
            command.Aggregate_id,
            ++max_sequence,
            ev));

        switch (command.Command)
        {
            case Get_box get_box:
                var handler = new Get_box_handler(filtered_stream, publish = );
                handler.Handle(get_box);
                break;
        }
    }
}

