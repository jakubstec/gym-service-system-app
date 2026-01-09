namespace gym_app.Services;

public interface IPriceStrategy
{
    decimal Calculate(decimal basePrice);
    string GetDiscountName();
}

public class NormalPriceStrategy : IPriceStrategy
{
    public decimal Calculate(decimal basePrice) => basePrice;
    public string GetDiscountName() => "Standard";
}

public class StudentDiscountStrategy : IPriceStrategy
{
    public decimal Calculate(decimal basePrice) => basePrice * 0.80m;
    public string GetDiscountName() => "Student (-20%)";
}

public class SeniorDiscountStrategy : IPriceStrategy
{
    public decimal Calculate(decimal basePrice) => basePrice * 0.70m;
    public string GetDiscountName() => "Senior (-30%)";
}
