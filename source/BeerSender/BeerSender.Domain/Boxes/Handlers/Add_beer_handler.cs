namespace BeerSender.Domain.Boxes.Handlers;

class Add_beer_handler : Command_handler<Add_beer, Box>
{
    public Add_beer_handler(
        IEnumerable<Event> event_stream,
        Action<Event> publish_event)
        : base(event_stream, publish_event)
    { }

    protected override IEnumerable<Event> Handle_command(
        Box aggregate,
        Add_beer command)
    {
        if ((aggregate.Capacity?.Number_of_spots ?? 0) 
            > aggregate.Contents.Count)
            yield return new Beer_added(command.Beer);
        else
            yield return new Beer_failed_to_add(
                Beer_failed_to_add.Fail_reason.Box_was_full);
    }
}