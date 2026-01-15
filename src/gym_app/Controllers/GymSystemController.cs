using gym_app.Abstractions;
using gym_app.Models;
using gym_app.Services.Initialization;
using gym_app.Services.Sessions;
using gym_app.Services.Tickets;

namespace gym_app.Controllers;

public class GymSystemController
{
    private readonly ISessionManagementService _sessionService;
    private readonly ITicketManagementService _ticketService;
    private readonly List<IObserver> _observers = [];

    public event Action? OnStateChanged;
    public event Action<string>? OnNewBooking;

    public List<TrainingSession> Sessions => _sessionService.GetAllSessions();
    public List<TicketData> GeneratedTickets => _ticketService.GetAllTickets();
    public List<Employee> Employees { get; private set; } = new();

    public GymSystemController()
    {
        var sessions = GymScheduleInitializer.InitializeSchedule();
        
        _sessionService = new SessionManagementService(sessions);
        _ticketService = new TicketManagementService(_sessionService);
        
        _sessionService.OnStateChanged += () => OnStateChanged?.Invoke();
        _ticketService.OnStateChanged += () => OnStateChanged?.Invoke();
        
        InitializeEmployees();
    }

    private void InitializeEmployees()
    {
        var employees = new[]
        {
            new Employee { Name = "Ola" },
            new Employee { Name = "Marek" },
            new Employee { Name = "Kasia" },
            new Employee { Name = "Piotr" },
            new Employee { Name = "Ania" },
            new Employee { Name = "Michał" }
        };

        foreach (var emp in employees)
        {
            Employees.Add(emp);
            _observers.Add(emp);
        }
    }

    private void NotifyObservers(string message)
    {
        foreach (var observer in _observers)
        {
            observer.Update(message);
        }
        OnNewBooking?.Invoke(message);
    }
    public void AddSession(TrainingSession session) 
        => _sessionService.AddSession(session);
    
    public void BookSession(string sessionId, string nickname)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null) return;

        _sessionService.BookSession(sessionId, nickname);
        
        string trainerName = session.InstructorName.Split(' ')[0];
        NotifyObservers($"Użytkownik {nickname} zapisał się na {session.Name} (Trener: {trainerName})");
    }

    public void RemoveSession(string id)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == id);
        var relatedTickets = GeneratedTickets
            .Where(t => t.RelatedSessionId == id)
            .ToList();
        
        foreach (var ticket in relatedTickets)
        {
            _ticketService.RemoveTicket(ticket.TicketId);
        }
        
        _sessionService.RemoveSession(id);
        
        if (session != null)
        {
            string trainerName = session.InstructorName.Split(' ')[0];
            NotifyObservers($"Zajęcia {session.Name} zostały usunięte (Trener: {trainerName})");
        }
    }

    public void ResignSession(string sessionId, string nickname)
    {
        var session = Sessions.FirstOrDefault(s => s.Id == sessionId);
        _sessionService.ResignSession(sessionId, nickname);
        
        if (session != null)
        {
            string trainerName = session.InstructorName.Split(' ')[0];
            NotifyObservers($"Użytkownik {nickname} zrezygnował z {session.Name} (Trener: {trainerName})");
        }
    }

    public void AddTicket(TicketData ticket)
    {
        _ticketService.AddTicket(ticket);
        
        if (!string.IsNullOrEmpty(ticket.RelatedSessionId))
        {
            var session = Sessions.FirstOrDefault(s => s.Id == ticket.RelatedSessionId);
            if (session != null)
            {
                string trainerName = session.InstructorName.Split(' ')[0];
                NotifyObservers($"Nowy bilet dla {ticket.OwnerNickname}: {ticket.ServiceName}");
            }
        }
    }
    
    public void RemoveTicket(string ticketId) 
        => _ticketService.RemoveTicket(ticketId);
}