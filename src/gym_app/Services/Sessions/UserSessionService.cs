using gym_app.Abstractions;
using gym_app.Models;

namespace gym_app.Services.Sessions;

public class UserSessionService : IUserSessionService
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