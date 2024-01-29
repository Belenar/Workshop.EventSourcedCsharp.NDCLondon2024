using BeerSender.Domain.Boxes;

namespace BeerSender.Tests.Boxes;

public partial class Box_test : TestBase
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

    protected Close_box Close_box()
    {
        return new Close_box();
    }

    protected Apply_shipping_label Apply_invalid_UPS_label()
    {
        return new Apply_shipping_label(
            new Shipping_label(Shipping_carrier.Ups, "ABC123"));
    }

    protected Apply_shipping_label Apply_valid_UPS_label()
    {
        return new Apply_shipping_label(
            new Shipping_label(Shipping_carrier.Ups, "DEF456"));
    }

    protected Ship_box Ship_box()
    {
        return new Ship_box();
    }
}