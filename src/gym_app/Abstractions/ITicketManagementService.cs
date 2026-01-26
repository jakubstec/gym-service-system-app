namespace gym_app.Abstractions;

using Models;

public interface ITicketManagementService
{
    event Action? OnStateChanged;
    
    void AddTicket(TicketData ticket);
    void RemoveTicket(string ticketId);
    List<TicketData> GetAllTickets();
}