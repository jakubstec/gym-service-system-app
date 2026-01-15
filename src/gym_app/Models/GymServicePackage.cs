namespace gym_app.Models;

public class GymServicePackage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal BasePrice { get; set; }
    public string ImageUrl { get; set; } = "logo.png";
}