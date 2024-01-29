namespace BeerSender.Domain.Boxes;

public record Get_box(int Desired_number_of_spots) : Command;
public record Add_beer(Bottle Beer) : Command;
public record Apply_shipping_label(Shipping_label Label) : Command;
public record Close_box : Command;
public record Ship_box : Command;