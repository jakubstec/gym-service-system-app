namespace gym_app.Models;

public class GymServiceOptions
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal BasePrice { get; set; }
    public string ImageUrl { get; set; } = "logo.png";
}

public class TrainingSession
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string InstructorName { get; set; } = "";
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public int MaxCapacity { get; set; }
    public int CurrentBookings { get; set; }
    public decimal Price { get; set; }
    public bool IsPersonal { get; set; }
    public List<string> Participants { get; set; } = new(); 
}

public class TicketData
{
    public string TicketId { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    public string ServiceName { get; set; } = "";
    public string OwnerNickname { get; set; } = "";
    public decimal PricePaid { get; set; }
    public DateTime ValidDate { get; set; }
    public string Details { get; set; } = "";
    public string? RelatedSessionId { get; set; }
}