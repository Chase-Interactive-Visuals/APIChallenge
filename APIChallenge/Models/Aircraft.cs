using System.Text.Json.Serialization;

namespace APIChallenge.Models
{
    [System.Serializable]
    public class Aircraft
    {
        public int AircraftId { get; set; }
        [JsonIgnore]
        public double DailyHours { get; set; }
        [JsonIgnore]
        public double CurrentHours { get; set; }
        public AircraftTasks? aircraftTasks { get; set; }
        public Aircraft(int aircraftID, double dailyHours, double currentHours)
        {
            AircraftId = aircraftID;
            DailyHours = dailyHours;
            CurrentHours = currentHours;
        }
    }
    
}
