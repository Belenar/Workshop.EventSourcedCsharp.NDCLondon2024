namespace BeerSender.Domain.Boxes;

public record Bottle(
    string Brewery,
    string Name,
    int Alcohol_percentage,
    int Volume_in_ml);