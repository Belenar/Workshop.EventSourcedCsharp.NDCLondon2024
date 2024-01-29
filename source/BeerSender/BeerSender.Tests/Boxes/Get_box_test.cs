namespace BeerSender.Tests.Boxes;

public class Get_box_test : Box_test
{
    [Theory]
    [InlineData(-1, 6)]
    [InlineData(0, 6)]
    [InlineData(5, 6)]
    [InlineData(6, 6)]
    [InlineData(7, 12)]
    [InlineData(11, 12)]
    [InlineData(12, 12)]
    [InlineData(13, 24)]
    [InlineData(23, 24)]
    [InlineData(24, 24)]
    [InlineData(25, 24)]
    public void Creates_box_with_correct_capacity(int desired_capacity, int resulting_capacity)
    {
        Given();

        When(
            Create_box_with_desired_capacity(desired_capacity));

        Expect(
            Box_created_with_capacity(resulting_capacity));
    }
}

public class Add_bottle_test : Box_test
{
    [Fact]
    public void Add_bottle_to_empty_box_succeeds()
    {
        Given(
            Box_created_with_capacity(24));

        When(
            Add_killer_belgian_beer());

        Expect(
            Killer_belgian_beer_added());
    }

    [Fact]
    public void Add_bottle_to_full_box_fails()
    {
        Given(
            Box_created_with_capacity(1),
            Killer_belgian_beer_added());

        When(
            Add_killer_belgian_beer());

        Expect(
            Beer_failed_to_add_because_box_was_full());
    }
}