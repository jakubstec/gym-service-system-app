namespace gym_app.Services.Initialization;

using gym_app.Models;

public static class GymScheduleInitializer
{
    public static List<TrainingSession> InitializeSchedule()
    {
        var sessions = new List<TrainingSession>();
        var today = DateTime.Today;

        for (int dayOffset = 0; dayOffset < 7; dayOffset++)
        {
            var date = today.AddDays(dayOffset);
            int dayOfWeek = (int)date.DayOfWeek;

            if (dayOfWeek >= 1 && dayOfWeek <= 5)
            {
                switch (dayOfWeek)
                {
                    case 1:
                        sessions.Add(SessionFactory.CreateGroupClass("Joga Poranna", "Ola", dayOffset, 9.0, 60));
                        sessions.Add(SessionFactory.CreateGroupClass("Zumba", "Kasia", dayOffset, 18.0, 60));
                        break;
                    case 2:
                        sessions.Add(SessionFactory.CreateGroupClass("Pilates", "Ola", dayOffset, 10.5, 60));
                        sessions.Add(SessionFactory.CreateGroupClass("Crossfit", "Marek", dayOffset, 19.0, 90));
                        break;
                    case 3:
                        sessions.Add(SessionFactory.CreateGroupClass("Rowery", "Ola", dayOffset, 12.0, 90));
                        sessions.Add(SessionFactory.CreateGroupClass("HIIT Express", "Kasia", dayOffset, 14.0, 60));
                        break;
                    case 4:
                        sessions.Add(SessionFactory.CreateGroupClass("Pilates", "Ola", dayOffset, 10.5, 60));
                        sessions.Add(SessionFactory.CreateGroupClass("Spinning", "Marek", dayOffset, 16.5, 90));
                        break;
                    case 5:
                        sessions.Add(SessionFactory.CreateGroupClass("Stretching", "Ola", dayOffset, 9.0, 60));
                        sessions.Add(SessionFactory.CreateGroupClass("Zumba Party", "Kasia", dayOffset, 18.0, 120));
                        break;
                }

                sessions.Add(SessionFactory.CreatePersonalTraining("Trening Personalny", "Piotr", dayOffset, 8.0));
                sessions.Add(SessionFactory.CreatePersonalTraining("Kulturystyka 1:1", "Ania", dayOffset, 13.0));
                sessions.Add(SessionFactory.CreatePersonalTraining("Wsparcie Formy", "Michał", dayOffset, 15.0));
            }
        }

        return sessions;
    }
}