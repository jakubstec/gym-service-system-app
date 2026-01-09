using gym_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using gym_app.Services;


namespace gym_app.Services;

public class GymSystemController
{
    public event Action? OnStateChanged;
    public event Action<string>? OnNewBooking;

    public List<TrainingSession> Sessions { get; private set; } = new();
    public List<TicketData> GeneratedTickets { get; private set; } = new();

    public GymSystemController()
{
    Sessions.Add(SessionFactory.CreateGroupClass("Joga Poranna", "Ola", 0, 9.0, 60));
    Sessions.Add(SessionFactory.CreateGroupClass("Zumba", "Kasia", 0, 18.0, 60));

    Sessions.Add(SessionFactory.CreateGroupClass("Pilates", "Ola", 1, 10.5, 60));
    Sessions.Add(SessionFactory.CreateGroupClass("Crossfit", "Marek", 1, 19.0, 90));

    Sessions.Add(SessionFactory.CreateGroupClass("Rowery", "Ola", 2, 12.0, 90));
    Sessions.Add(SessionFactory.CreateGroupClass("HIIT Express", "Kasia", 2, 14.0, 60));

    Sessions.Add(SessionFactory.CreateGroupClass("Pilates", "Ola", 3, 10.5, 60));
    Sessions.Add(SessionFactory.CreateGroupClass("Spinning", "Marek", 3, 16.5, 90));

    Sessions.Add(SessionFactory.CreateGroupClass("Stretching", "Ola", 4, 9.0, 60));
    Sessions.Add(SessionFactory.CreateGroupClass("Zumba Party", "Kasia", 4, 18.0, 120));

    for (int day = 0; day < 5; day++)
    {
        Sessions.Add(SessionFactory.CreatePersonalTraining("Trening Personalny", "Piotr (Trener)", day, 8.0));
        Sessions.Add(SessionFactory.CreatePersonalTraining("Kulturystyka 1:1", "Ania (Pro)", day, 13.0));
        Sessions.Add(SessionFactory.CreatePersonalTraining("Trening Personalny", "Piotr (Trener)", day, 8.0));

        Sessions.Add(SessionFactory.CreatePersonalTraining("Wsparcie Formy", "Michał (Trener)", day, 15.0));
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