using gym_app.Abstractions;

namespace gym_app.Models;

public class Employee : IObserver
{
    public string Name { get; set; } = "";
    public List<string> Notifications { get; set; } = new();

    public void Update(string message)
    {
        if (message.Contains(Name.Split(' ')[0]))
        {
            Notifications.Insert(0, $"{DateTime.Now:HH:mm} - {message}");
            
            if (Notifications.Count > 50)
            {
                Notifications.RemoveAt(Notifications.Count - 1);
            }
        }
    }
}