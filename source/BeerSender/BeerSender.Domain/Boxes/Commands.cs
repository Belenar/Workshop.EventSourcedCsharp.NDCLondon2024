namespace BeerSender.Domain.Boxes;

public record Get_box(int Desired_number_of_spots);
public record Add_beer(Bottle Beer);
public record Apply_shipping_label(Shipping_label Label);
public record Close_box();
public record Ship_box();