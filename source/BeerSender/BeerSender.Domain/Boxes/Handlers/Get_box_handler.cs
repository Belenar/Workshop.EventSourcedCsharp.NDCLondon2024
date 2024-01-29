namespace BeerSender.Domain.Boxes.Handlers;

class Get_box_handler : Command_handler<Get_box, Box>
{
    public Get_box_handler(
        IEnumerable<object> event_stream, 
        Action<object> publish_event) 
        : base(event_stream, publish_event)
    { }

    protected override IEnumerable<object> Handle_command(
        Box aggregate,
        Get_box command)
    {
        var capacity = Capacity.Create(command.Desired_number_of_spots);
        yield return new Box_created(capacity);
    }
}