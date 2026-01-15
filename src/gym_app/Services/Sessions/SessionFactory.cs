using gym_app.Models;

namespace gym_app.Services;

public static class SessionFactory
{
    private static readonly DateTime BaseDate = DateTime.Today;
    
    public static TrainingSession CreateGroupClass(string name, string instructor, int dayOffset, double startHour, int durationMinutes)
    {
        var date = BaseDate.AddDays(dayOffset);
        var startTime = date.AddHours(startHour);

        return new TrainingSession
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            InstructorName = instructor,
            StartTime = startTime,
            DurationMinutes = durationMinutes,
            MaxCapacity = 20,
            Price = 15,    
            IsPersonal = false,
            CurrentBookings = 0,
            Participants = new List<string>()
        };
    }
    
    public static TrainingSession CreatePersonalTraining(string name, string trainer, int dayOffset, double startHour)
    {
        var date = BaseDate.AddDays(dayOffset);
        var startTime = date.AddHours(startHour);

        return new TrainingSession
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            InstructorName = trainer,
            StartTime = startTime,
            DurationMinutes = 60,
            MaxCapacity = 1,      
            Price = 100,
            IsPersonal = true,
            CurrentBookings = 0,
            Participants = new List<string>()
        };
    }
}