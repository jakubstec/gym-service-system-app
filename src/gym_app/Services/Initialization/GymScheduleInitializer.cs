namespace gym_app.Services.Initialization;

using gym_app.Models;

public static class GymScheduleInitializer
{
    public static List<TrainingSession> InitializeSchedule()
    {
        var sessions = new List<TrainingSession>
        {
            SessionFactory.CreateGroupClass("Joga Poranna", "Ola", 0, 9.0, 60),
            SessionFactory.CreateGroupClass("Zumba", "Kasia", 0, 18.0, 60),
            SessionFactory.CreateGroupClass("Pilates", "Ola", 1, 10.5, 60),
            SessionFactory.CreateGroupClass("Crossfit", "Marek", 1, 19.0, 90),
            SessionFactory.CreateGroupClass("Rowery", "Ola", 2, 12.0, 90),
            SessionFactory.CreateGroupClass("HIIT Express", "Kasia", 2, 14.0, 60),
            SessionFactory.CreateGroupClass("Pilates", "Ola", 3, 10.5, 60),
            SessionFactory.CreateGroupClass("Spinning", "Marek", 3, 16.5, 90),
            SessionFactory.CreateGroupClass("Stretching", "Ola", 4, 9.0, 60),
            SessionFactory.CreateGroupClass("Zumba Party", "Kasia", 4, 18.0, 120),
        };

        for (int day = 0; day < 5; day++)
        {
            sessions.Add(SessionFactory.CreatePersonalTraining("Trening Personalny", "Piotr (Trener)", day, 8.0));
            sessions.Add(SessionFactory.CreatePersonalTraining("Kulturystyka 1:1", "Ania (Pro)", day, 13.0));
            sessions.Add(SessionFactory.CreatePersonalTraining("Trening Personalny", "Piotr (Trener)", day, 8.0));
            sessions.Add(SessionFactory.CreatePersonalTraining("Wsparcie Formy", "Michał (Trener)", day, 15.0));
        }

        return sessions;
    }
}