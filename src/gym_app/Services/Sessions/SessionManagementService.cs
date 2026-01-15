namespace gym_app.Services.Sessions;

using gym_app.Abstractions;
using gym_app.Models;

public class SessionManagementService : ISessionManagementService
{
    private readonly List<TrainingSession> _sessions;
    
    public event Action? OnStateChanged;
    public event Action<string>? OnNewBooking;

    public SessionManagementService(List<TrainingSession> sessions)
    {
        _sessions = sessions;
    }

    public List<TrainingSession> GetAllSessions() => _sessions;

    public void AddSession(TrainingSession session)
    {
        _sessions.Add(session);
        OnStateChanged?.Invoke();
    }

    public void BookSession(string sessionId, string nickname)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session != null && session.CurrentBookings < session.MaxCapacity)
        {
            session.CurrentBookings++;
            session.Participants.Add(nickname);
            
            OnNewBooking?.Invoke($"Użytkownik {nickname} zapisał się na {session.Name}");
            OnStateChanged?.Invoke();
        }
    }

    public void RemoveSession(string sessionId)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session != null)
        {
            _sessions.Remove(session);
            OnStateChanged?.Invoke();
        }
    }

    public void ResignSession(string sessionId, string nickname)
    {
        var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session != null && session.Participants.Contains(nickname))
        {
            session.CurrentBookings--;
            session.Participants.Remove(nickname);
            OnStateChanged?.Invoke();
        }
    }
}