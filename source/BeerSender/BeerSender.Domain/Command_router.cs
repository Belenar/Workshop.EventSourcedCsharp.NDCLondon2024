using System.Reflection;

namespace BeerSender.Domain;

public class Command_router
{
    private readonly Func<Guid, IEnumerable<Event_message>> _event_stream;
    private readonly Action<Event_message> _publish_event;

    public Command_router(
        Func<Guid, IEnumerable<Event_message>> event_stream,
        Action<Event_message> publish_event)
    {
        _event_stream = event_stream;
        _publish_event = publish_event;
    }

    public void Handle(Command_message command)
    {
        var filtered_stream = _event_stream(command.Aggregate_id);
        var max_sequence = filtered_stream.Select(e => e.Sequence).DefaultIfEmpty().Max();
        Action<Event> publish = (ev => _publish_event(new Event_message(
            command.Aggregate_id,
            ++max_sequence,
            ev)));

        var command_type = command.Command.GetType();
        var handler_type = Command_handlers[command_type].Handler_type;
        var handle_method = Command_handlers[command_type].Handle_method;

        var instance = Activator.CreateInstance(handler_type, 
            filtered_stream.Select(e => e.Event), publish);

        handle_method.Invoke(instance, new[] { command.Command });
    }

    private static readonly Dictionary<Type, (Type Handler_type, MethodInfo Handle_method)>
        Command_handlers = new();

    static Command_router()
    {
        // Get all handlers with base type Command_handler (only 1st level base type)
        var handler_types = typeof(Command_router).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .Where(t => t.BaseType?.Name == typeof(Command_handler<,>).Name);

        // Add them to the registry: command type -> handler type, apply & handle
        foreach (var handler_type in handler_types)
        {
            var command_type = handler_type.BaseType?.GenericTypeArguments.First();
            var handle_method = handler_type.GetMethod("Handle")!;
            Command_handlers.Add(command_type!, (handler_type, handle_method));
        }
    }
}

