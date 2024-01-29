namespace BeerSender.Domain.Boxes;

class Box: Aggregate
{
    public Capacity? Capacity { get; private set; }
    public Shipping_label? Shipping_label { get; private set; }
    public List<Bottle> Contents { get; } = new();
    public bool Closed { get; private set; }
    public bool Shipped { get; private set; }

    // Get box
    public void Apply(Box_created @event)
    {
        Capacity = @event.Capacity;
    }

    // Add beer
    public void Apply(Beer_added @event)
    {
        Contents.Add(@event.Beer);
    }
    public void Apply(Beer_failed_to_add @event) { }

    // Apply shipping label
    public void Apply(Label_applied @event)
    {
        Shipping_label = @event.Label;
    }
    public void Apply(Label_was_invalid @event) { }

    // Close box
    public void Apply(Box_closed @event)
    {
        Closed = true;
    }
    public void Apply(Box_failed_to_close @event) { }

    // Ship box
    public void Apply(Box_shipped @event)
    {
        Shipped = true;
    }
    public void Apply(Box_was_not_ready @event) { }
}