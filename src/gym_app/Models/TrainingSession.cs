namespace gym_app.Models;

public class TrainingSession
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string InstructorName { get; set; } = "";
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentBookings { get; set; }
    public decimal Price { get; set; }
    public bool IsPersonal { get; set; }
    public List<string> Participants { get; set; } = new();
}