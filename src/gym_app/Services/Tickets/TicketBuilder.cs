using gym_app.Models;
using gym_app.Abstractions;
using gym_app.Services.Pricing;

namespace gym_app.Services.Tickets;


public static class TicketPricing
{
    public const decimal BasePrice = 20m;
    public const decimal SaunaAddon = 10m;
    public const decimal PoolAddon = 15m;
}

public class OpenTicketBuilder
{
    private readonly TicketData _ticket;
    private decimal _price = TicketPricing.BasePrice;
    private readonly List<string> _features = [];

    public OpenTicketBuilder(string ownerNickname)
    {
        _ticket = new TicketData
        {
            TicketId = Guid.NewGuid().ToString(),
            OwnerNickname = ownerNickname,
            ValidDate = DateTime.Now,
            ServiceName = "Wejście OPEN"
        };
        _features.Add("Siłownia");
    }

    public OpenTicketBuilder AddSauna()
    {
        _features.Add("Sauna");
        _price += TicketPricing.SaunaAddon;
        return this;
    }

    public OpenTicketBuilder AddPool()
    {
        _features.Add("Basen");
        _price += TicketPricing.PoolAddon;
        return this;
    }

    public OpenTicketBuilder ApplyDiscount(IPriceStrategy strategy)
    {
        _price = strategy.Calculate(_price);
        
        if (strategy is not NormalPriceStrategy)
            _features.Add($"Zniżka: {strategy.GetDiscountName()}");
            
        return this;
    }

    public TicketData Build()
    {
        _ticket.PricePaid = Math.Round(_price, 2);
        _ticket.Details = string.Join(" + ", _features);
        return _ticket;
    }
}