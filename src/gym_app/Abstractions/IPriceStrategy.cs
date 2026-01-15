public interface IPriceStrategy
{
    decimal Calculate(decimal basePrice);
    string GetDiscountName();
}