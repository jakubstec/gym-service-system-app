namespace gym_app.Abstractions;

using Models;

public interface ISessionManagementService
{
    event Action? OnStateChanged;
    void BookSession(string sessionId, string nickname);
    void ResignSession(string sessionId, string nickname);
    void AddSession(TrainingSession session);
    void RemoveSession(string sessionId);
    List<TrainingSession> GetAllSessions();
}