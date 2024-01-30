namespace BeerSender.Domain.Boxes;

// Get box
public record Box_created(Capacity Capacity) : Event;

// Add beer
public record Beer_added(Bottle Beer) : Event;
public record Beer_failed_to_add(Beer_failed_to_add.Fail_reason Reason) : Event
{
    public enum Fail_reason
    {
        Box_was_full
    }
}

// Close Box
public record Box_closed : Event;
public record Box_failed_to_close(Box_failed_to_close.Fail_reason Reason) : Event
{
    public enum Fail_reason
    {
        Box_was_empty
    }
}

// Add label
public record Label_was_invalid : Event;
public record Label_applied(Shipping_label Label) : Event;

// Ship box
public record Box_shipped : Event;
public record Box_was_not_ready(Box_was_not_ready.Fail_reason Reason) : Event
{
    public enum Fail_reason
    {
        Box_was_not_closed,
        Box_has_no_shipping_label
    }
}