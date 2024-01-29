namespace BeerSender.Tests.Boxes;

public class Apply_label_test : Box_test
{
    [Fact]
    public void Apply_valid_label_succeeds()
    {
        Given(
            Box_created_with_capacity(24));

        When(
            Apply_valid_UPS_label());

        Expect(
            UPS_label_applied_to_box());
    }

    [Fact]
    public void Applying_invalid_label_fails()
    {
        Given(
            Box_created_with_capacity(24));

        When(
            Apply_invalid_UPS_label());

        Expect(
            Label_not_applied_because_it_was_invalid());
    }
}