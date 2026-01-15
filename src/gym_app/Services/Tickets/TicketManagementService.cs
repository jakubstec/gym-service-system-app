namespace gym_app.Services.Tickets;

using gym_app.Abstractions;
using gym_app.Models;

public class TicketManagementService : ITicketManagementService
{
    private readonly List<TicketData> _tickets;
    private readonly ISessionManagementService _sessionService;
    
    public event Action? OnStateChanged;

    public TicketManagementService(ISessionManagementService sessionService)
    {
        _tickets = new List<TicketData>();
        _sessionService = sessionService;
    }

    public List<TicketData> GetAllTickets() => _tickets;

    public void AddTicket(TicketData ticket)
    {
        _tickets.Add(ticket);
        OnStateChanged?.Invoke();
    }

    public void RemoveTicket(string ticketId)
    {
        var ticket = _tickets.FirstOrDefault(t => t.TicketId == ticketId);
        if (ticket != null)
        {
            if (!string.IsNullOrEmpty(ticket.RelatedSessionId))
            {
                _sessionService.ResignSession(ticket.RelatedSessionId, ticket.OwnerNickname);
            }
            
            _tickets.Remove(ticket);
            OnStateChanged?.Invoke();
        }
    }
}