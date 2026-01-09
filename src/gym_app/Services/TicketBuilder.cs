using gym_app.Models;

namespace gym_app.Services;

public class OpenTicketBuilder
{
    private TicketData _ticket;
    private decimal _basePrice = 20m;
    private List<string> _features = new();

    public OpenTicketBuilder(string ownerNickname)
    {
        _ticket = new TicketData
        {
            TicketId = Guid.NewGuid().ToString(),
            OwnerNickname = ownerNickname ?? "Gość",
            ValidDate = DateTime.Now,
            ServiceName = "Wejście OPEN"
        };
        _features.Add("Siłownia");
    }

    public OpenTicketBuilder AddSauna()
    {
        _features.Add("Sauna");
        _basePrice += 10m;
        return this;
    }

    public OpenTicketBuilder AddPool()
    {
        _features.Add("Basen");
        _basePrice += 15m;
        return this;
    }

    public OpenTicketBuilder ApplyDiscount(IPriceStrategy strategy)
    {
        _basePrice = strategy.Calculate(_basePrice);
        
        if (strategy is not NormalPriceStrategy)
            _features.Add($"Zniżka: {strategy.GetDiscountName()}");
            
        return this;
    }

    public TicketData Build()
    {
        _ticket.PricePaid = Math.Round(_basePrice, 2);
        _ticket.Details = string.Join(" + ", _features);
        return _ticket;
    }
}