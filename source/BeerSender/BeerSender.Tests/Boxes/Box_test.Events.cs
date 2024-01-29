using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public partial class Box_test
{
    protected Box_created Box_created_with_capacity(int capacity)
    {
        return new Box_created(new Capacity(capacity));
    }

    protected Beer_added Killer_belgian_beer_added()
    {
        return new Beer_added(new Bottle(
            "Gouden Carolus",
            "Quadrupel Whisky Infused",
            11.7M,
            330));
    }

    protected Beer_failed_to_add Beer_failed_to_add_because_box_was_full()
    {
        return new Beer_failed_to_add(Beer_failed_to_add.Fail_reason.Box_was_full);
    }

    protected Box_closed Box_closed()
    {
        return new Box_closed();
    }

    protected Box_failed_to_close Box_failed_to_close_because_it_was_empty()
    {
        return new Box_failed_to_close(
            Box_failed_to_close.Fail_reason.Box_was_empty);
    }

    protected Box_shipped Box_shipped()
    {
        return new Box_shipped();
    }

    protected Box_was_not_ready Box_not_shipped_because_it_was_not_closed()
    {
        return new Box_was_not_ready(
            Box_was_not_ready.Fail_reason.Box_was_not_closed);
    }

    protected Box_was_not_ready Box_not_shipped_because_it_has_no_label()
    {
        return new Box_was_not_ready(
            Box_was_not_ready.Fail_reason.Box_has_no_shipping_label);
    }

    protected Label_applied UPS_label_applied_to_box()
    {
        return new Label_applied(
            new Shipping_label(Shipping_carrier.Ups, "DEF456"));
    }

    protected Label_was_invalid Label_not_applied_because_it_was_invalid()
    {
        return new Label_was_invalid();
    }
}