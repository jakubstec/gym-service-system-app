namespace gym_app.Models;

public class TicketData
{
    public string TicketId { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    public string OwnerNickname { get; set; } = "";
    public DateTime ValidDate { get; set; }
    public string ServiceName { get; set; } = "";
    public decimal PricePaid { get; set; }
    public string Details { get; set; } = "";
    public string? RelatedSessionId { get; set; }
}