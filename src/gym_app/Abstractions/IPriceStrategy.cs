namespace gym_app.Abstractions;

public interface IPriceStrategy
{
    decimal Calculate(decimal basePrice);
    string GetDiscountName();
}