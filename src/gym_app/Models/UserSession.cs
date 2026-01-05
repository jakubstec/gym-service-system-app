namespace gym_app.Models;

public enum UserRole
{
    Client,
    Employee
}

public class UserSession
{
    public string Nickname { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Client;
}