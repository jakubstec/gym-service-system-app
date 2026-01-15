namespace gym_app.Models;

public class Employee : IObserver
{
    public string Name { get; set; } = "";
    public List<string> Notifications { get; set; } = [];

    public void Update(string message)
    {
        Notifications.Insert(0, $"{DateTime.Now:HH:mm} - {message}");
    }
}