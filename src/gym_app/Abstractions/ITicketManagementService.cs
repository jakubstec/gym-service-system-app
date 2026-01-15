namespace gym_app.Abstractions;

using gym_app.Models;

public interface ITicketManagementService
{
    event Action? OnStateChanged;
    
    void AddTicket(TicketData ticket);
    void RemoveTicket(string ticketId);
    List<TicketData> GetAllTickets();
}