namespace BeerSender.Tests.Boxes;

public class Close_box_test : Box_test
{
    [Fact]
    public void Close_box_with_content_succeeds()
    {
        Given(
            Box_created_with_capacity(24),
            Killer_belgian_beer_added());

        When(
            Close_box());

        Expect(
            Box_closed());
    }

    [Fact]
    public void Close_empty_box_fails()
    {
        Given(
            Box_created_with_capacity(24));

        When(
            Close_box());

        Expect(
            Box_failed_to_close_because_it_was_empty());
    }
}