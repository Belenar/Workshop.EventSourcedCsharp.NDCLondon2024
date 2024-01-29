using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public class Box_test : TestBase
{
    protected Get_box Create_box_with_desired_capacity(int desired_capacity)
    {
        return new Get_box(desired_capacity);
    }

    protected Add_beer Add_killer_belgian_beer()
    {
        return new Add_beer(new Bottle(
            "Gouden Carolus",
            "Quadrupel Whisky Infused",
            11.7M,
            330));
    }

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
}