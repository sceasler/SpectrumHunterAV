using System;

namespace SpectrumHunterClient
{
    public class SignalData
    {
        public SignalData()
        {
            id = 0;
            bearing = 0.0f;
            age = 0;
        }

        public SignalData(int id, float bearing, int age)
        {
            this.id = id;
            this.bearing = bearing;
            this.age = age;
        }

        public int id { get; set; }
        public float bearing { get; set; }
        public int age { get; set; }
        // public SignalHistory (tuple of messageStamp & bearing)

        public string GetBearing()
        {
            string bearingString = String.Empty;

            bearingString = bearing.ToString() + " degrees";

            return bearingString;
        }

        public string GetAge()
        {
            string ageString = String.Empty;

            if (age < 1)
            {
                ageString = $"<1 sec";
            }
            else if (age >= 60)
            {
                int minutes = age / 60;
                ageString = $">{ minutes } min";
            }
            else
            {
                ageString = $"{ age } sec";
            }
            return ageString;
        }
    }
}

