using gym_app.Models;

namespace gym_app.Abstractions;

public interface IUserSessionService
{
    string Nickname { get; set; }
    UserRole? Role { get; set; }
    bool IsLoggedIn { get; }
    event Action? OnSessionChanged;
    void Login(string nick, UserRole role);
    void Logout();
}