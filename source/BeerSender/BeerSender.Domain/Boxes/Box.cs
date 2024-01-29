namespace BeerSender.Domain.Boxes;

class Box: Aggregate
{
    public Capacity? Capacity { get; private set; }
    public List<Bottle> Contents { get; } = new();

    public void Apply(Box_created @event)
    {
        Capacity = @event.Capacity;
    }

    public void Apply(Beer_added @event)
    {
        Contents.Add(@event.Beer);
    }
}