namespace BeerSender.Domain.Boxes;

public record Shipping_label(
    Shipping_carrier Carrier,
    string Tracking_code)
{
    public bool Is_valid()
    {
        switch(Carrier)
        {
            case Shipping_carrier.Fedex:
                return Tracking_code.StartsWith("ABC");
            case Shipping_carrier.Ups:
                return Tracking_code.StartsWith("DEF");
            case Shipping_carrier.Bpost:
                return Tracking_code.StartsWith("GHI");
            default:
                return false;
        }
    }
}