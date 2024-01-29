namespace BeerSender.Domain.Boxes;

// Get box
public record Box_created(Capacity Capacity);

// Add beer
public record Beer_added(Bottle Beer);
public record Beer_failed_to_add(Beer_failed_to_add.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Box_was_full
    }
}

// Close Box
public record Box_closed();
public record Box_failed_to_close(Box_failed_to_close.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Box_was_empty
    }
}

// Add label
public record Label_was_invalid(string Reason);
public record Label_applied(Shipping_label Label);

// Ship box
public record Box_shipped();
public record Box_was_not_ready(Box_was_not_ready.Fail_reason Reason)
{
    public enum Fail_reason
    {
        Box_was_not_closed,
        Box_has_no_shipping_label
    }
}