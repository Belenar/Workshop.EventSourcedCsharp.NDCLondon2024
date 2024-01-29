namespace BeerSender.Domain.Boxes;

public record Capacity(int Number_of_spots)
{
    public static Capacity Create(int desired_number_of_spots)
    {
        return desired_number_of_spots switch
        {
            <= 6 => new Capacity(6),
            <= 12 => new Capacity(12),
            <= 24 => new Capacity(24),
            _ => new Capacity(24)
        };
    }
}