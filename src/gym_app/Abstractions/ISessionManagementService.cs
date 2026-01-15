namespace gym_app.Abstractions;

using gym_app.Models;

public interface ISessionManagementService
{
    event Action? OnStateChanged;
    event Action<string>? OnNewBooking;
    
    void BookSession(string sessionId, string nickname);
    void ResignSession(string sessionId, string nickname);
    void AddSession(TrainingSession session);
    void RemoveSession(string sessionId);
    List<TrainingSession> GetAllSessions();
}