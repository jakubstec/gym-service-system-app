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

    public event Action? OnStateChanged;
    public event Action<string>? OnNewBooking;

    public List<TrainingSession> Sessions => _sessionService.GetAllSessions();
    public List<TicketData> GeneratedTickets => _ticketService.GetAllTickets();

    public GymSystemController()
    {
        var sessions = GymScheduleInitializer.InitializeSchedule();
        
        _sessionService = new SessionManagementService(sessions);
        _ticketService = new TicketManagementService(_sessionService);
        
        _sessionService.OnStateChanged += () => OnStateChanged?.Invoke();
        _sessionService.OnNewBooking += (message) => OnNewBooking?.Invoke(message);
        _ticketService.OnStateChanged += () => OnStateChanged?.Invoke();
    }

    // delegate session operations to SessionManagementService
    public void AddSession(TrainingSession session) 
        => _sessionService.AddSession(session);
    
    public void BookSession(string sessionId, string nickname) 
        => _sessionService.BookSession(sessionId, nickname);

    public void RemoveSession(string id)
    {
        var relatedTickets = GeneratedTickets
            .Where(t => t.RelatedSessionId == id)
            .ToList();
        
        foreach (var ticket in relatedTickets)
        {
            _ticketService.RemoveTicket(ticket.TicketId);
        }
        
        _sessionService.RemoveSession(id);
    }

    public void ResignSession(string sessionId, string nickname) 
        => _sessionService.ResignSession(sessionId, nickname);

    // delegate ticket operations to TicketManagementService
    public void AddTicket(TicketData ticket) 
        => _ticketService.AddTicket(ticket);
    
    public void RemoveTicket(string ticketId) 
        => _ticketService.RemoveTicket(ticketId);
}