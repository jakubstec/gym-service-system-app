using gym_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gym_app.Services;

public class GymSystemController
{
    public event Action? OnStateChanged;
    public event Action<string>? OnNewBooking;

    public List<TrainingSession> Sessions { get; private set; } = new();
    public List<TicketData> GeneratedTickets { get; private set; } = new();

    public GymSystemController()
{
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Joga Poranna", InstructorName = "Ola", StartTime = DateTime.Today.AddHours(9), DurationMinutes = 60, MaxCapacity = 15, CurrentBookings = 0, Price = 20, IsPersonal = false });
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Zumba", InstructorName = "Kasia", StartTime = DateTime.Today.AddHours(18), DurationMinutes = 60, MaxCapacity = 20, CurrentBookings = 0, Price = 15, IsPersonal = false });
    
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Pilates", InstructorName = "Ola", StartTime = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(30), DurationMinutes = 60, MaxCapacity = 12, CurrentBookings = 0, Price = 20, IsPersonal = false });
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Crossfit", InstructorName = "Marek", StartTime = DateTime.Today.AddDays(1).AddHours(19), DurationMinutes = 90, MaxCapacity = 15, CurrentBookings = 0, Price = 25, IsPersonal = false });
    
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Boks", InstructorName = "Marek", StartTime = DateTime.Today.AddDays(2).AddHours(12), DurationMinutes = 60, MaxCapacity = 10, CurrentBookings = 0, Price = 30, IsPersonal = false });
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "HIIT Express", InstructorName = "Jacek", StartTime = DateTime.Today.AddDays(2).AddHours(17).AddMinutes(30), DurationMinutes = 30, MaxCapacity = 25, CurrentBookings = 0, Price = 15, IsPersonal = false });

    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Trening Siłowy", InstructorName = "Jacek", StartTime = DateTime.Today.AddDays(3).AddHours(8), DurationMinutes = 60, MaxCapacity = 20, CurrentBookings = 0, Price = 20, IsPersonal = false });
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Spinning", InstructorName = "Ola", StartTime = DateTime.Today.AddDays(3).AddHours(18), DurationMinutes = 60, MaxCapacity = 15, CurrentBookings = 0, Price = 20, IsPersonal = false });
    
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Stretching", InstructorName = "Ola", StartTime = DateTime.Today.AddDays(4).AddHours(11), DurationMinutes = 60, MaxCapacity = 15, CurrentBookings = 0, Price = 20, IsPersonal = false });
    Sessions.Add(new TrainingSession { Id = Guid.NewGuid().ToString(), Name = "Zumba Party", InstructorName = "Kasia", StartTime = DateTime.Today.AddDays(4).AddHours(17), DurationMinutes = 120, MaxCapacity = 30, CurrentBookings = 0, Price = 15, IsPersonal = false });

    for (int day = 0; day < 5; day++)
    {
        Sessions.Add(new TrainingSession { 
            Id = Guid.NewGuid().ToString(), Name = "Trening Personalny", InstructorName = "Piotr (Trener)", 
            StartTime = DateTime.Today.AddDays(day).AddHours(8), DurationMinutes = 60, 
            MaxCapacity = 1, CurrentBookings = 0, Price = 100, IsPersonal = true 
        });
        
        Sessions.Add(new TrainingSession { 
            Id = Guid.NewGuid().ToString(), Name = "Kulturystyka 1:1", InstructorName = "Ania (Pro)", 
            StartTime = DateTime.Today.AddDays(day).AddHours(13), DurationMinutes = 60, 
            MaxCapacity = 1, CurrentBookings = 0, Price = 120, IsPersonal = true 
        });
        
        Sessions.Add(new TrainingSession { 
            Id = Guid.NewGuid().ToString(), Name = "Wsparcie Formy", InstructorName = "Michał (Coach)", 
            StartTime = DateTime.Today.AddDays(day).AddHours(15).AddMinutes(30), DurationMinutes = 90, 
            MaxCapacity = 1, CurrentBookings = 0, Price = 150, IsPersonal = true 
        });
    }
}


    

    public void AddSession(TrainingSession session)
    {
        Sessions.Add(session);
        NotifyStateChanged();
    }
    
    public void BookSession(string sessionId, string nickname)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session != null && session.CurrentBookings < session.MaxCapacity)
        {
            session.CurrentBookings++;
            session.Participants.Add(nickname);
            
            OnNewBooking?.Invoke($"Użytkownik {nickname} zapisał się na {session.Name}");
            NotifyStateChanged();
        }
    }

    public void RemoveSession(string id)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == id);
        if (session != null)
        {
            GeneratedTickets.RemoveAll(t => t.RelatedSessionId == id);
        
            Sessions.Remove(session);
            NotifyStateChanged();
        }
    }

    public void ResignSession(string sessionId, string nickname)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session != null && session.Participants.Contains(nickname))
        {
            session.CurrentBookings--;
            session.Participants.Remove(nickname);
            NotifyStateChanged();
        }
    }

    public void AddTicket(TicketData ticket)
    {
        GeneratedTickets.Add(ticket);
        NotifyStateChanged();
    }
    
    public void RemoveTicket(string ticketId)
    {
        var ticket = GeneratedTickets.FirstOrDefault(t => t.TicketId == ticketId);
        if (ticket != null)
        {
            if (!string.IsNullOrEmpty(ticket.RelatedSessionId))
            {
                ResignSession(ticket.RelatedSessionId, ticket.OwnerNickname);
            }

            GeneratedTickets.Remove(ticket);
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}