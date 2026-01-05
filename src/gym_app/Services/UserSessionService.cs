namespace gym_app.Services;
using gym_app.Models;

public class UserSessionService
{
    public string Nickname { get; set; } = "";
    public UserRole? Role { get; set; }
    public bool IsLoggedIn => Role != null;

    public event Action? OnSessionChanged;

    public void Login(string nick, UserRole role)
    {
        Nickname = nick;
        Role = role;
        OnSessionChanged?.Invoke();
    }

    public void Logout()
    {
        Nickname = "";
        Role = null;
        OnSessionChanged?.Invoke();
    }
}