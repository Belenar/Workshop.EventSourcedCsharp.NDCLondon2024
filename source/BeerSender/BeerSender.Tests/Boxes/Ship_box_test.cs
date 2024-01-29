namespace BeerSender.Tests.Boxes;

public class Ship_box_test : Box_test
{
    [Fact]
    public void Shipping_closed_box_with_label_succeeds()
    {
        Given(
            Box_created_with_capacity(24),
            Killer_belgian_beer_added(),
            Box_closed(),
            UPS_label_applied_to_box());

        When(
            Ship_box());

        Expect(
            Box_shipped());
    }

    [Fact]
    public void Shipping_open_box_with_label_fails()
    {
        Given(
            Box_created_with_capacity(24),
            Killer_belgian_beer_added(),
            UPS_label_applied_to_box());

        When(
            Ship_box());

        Expect(
            Box_not_shipped_because_it_was_not_closed());
    }

    [Fact]
    public void Shipping_closed_box_without_label_fails()
    {
        Given(
            Box_created_with_capacity(24),
            Killer_belgian_beer_added(),
            Box_closed());

        When(
            Ship_box());

        Expect(
            Box_not_shipped_because_it_has_no_label());
    }

    [Fact]
    public void Shipping_open_box_without_label_fails_for_2_reasons()
    {
        Given(
            Box_created_with_capacity(24),
            Killer_belgian_beer_added());

        When(
            Ship_box());

        Expect(
            Box_not_shipped_because_it_was_not_closed(),
            Box_not_shipped_because_it_has_no_label());
    }
}