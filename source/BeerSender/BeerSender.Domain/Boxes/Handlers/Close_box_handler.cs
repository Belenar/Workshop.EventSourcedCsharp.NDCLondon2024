namespace BeerSender.Domain.Boxes.Handlers;

class Close_box_handler : Command_handler<Close_box, Box>
{
    public Close_box_handler(
        IEnumerable<object> event_stream,
        Action<object> publish_event)
        : base(event_stream, publish_event)
    { }

    protected override IEnumerable<object> Handle_command(
        Box aggregate,
        Close_box command)
    {
        if (aggregate.Contents.Any())
            yield return new Box_closed();
        else
            yield return new Box_failed_to_close(
                Box_failed_to_close.Fail_reason.Box_was_empty);
    }
}