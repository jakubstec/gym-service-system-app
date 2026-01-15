using gym_app.Models;

namespace gym_app.Services;


public static class TicketPricing
{
    public const decimal BASE_PRICE = 20m;
    public const decimal SAUNA_ADDON = 10m;
    public const decimal POOL_ADDON = 15m;
}

public class OpenTicketBuilder
{
    private TicketData _ticket;
    private decimal _price = TicketPricing.BASE_PRICE;
    private List<string> _features = [];

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
        _price += TicketPricing.SAUNA_ADDON;
        return this;
    }

    public OpenTicketBuilder AddPool()
    {
        _features.Add("Basen");
        _price += TicketPricing.POOL_ADDON;
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