using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroSliceApplication
{
    internal class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int HighScoreRank { get; set; }
        public DateTime FirstDate { get; set; }
        public int PizzasConsumed { get; set; }
        public int BowlingHighScore { get; set; }
        public Boolean EmploymentStatus { get; set; }
        public string SlushyFlavorPreference { get; set; }
        public int SlushiesConsumed { get; set; }

        public Customer(string name, int age, int highScoreRank, DateTime startingDate,
                 int pizzasConsumed, int bowlingHighScore, Boolean employmentStatus,
                 string slushyFlavorPreference, int slushiesConsumed)
        {
            Name = name;
            Age = age;
            HighScoreRank = highScoreRank;
            FirstDate = startingDate;
            PizzasConsumed = pizzasConsumed;
            BowlingHighScore = bowlingHighScore;
            EmploymentStatus = employmentStatus;
            SlushyFlavorPreference = slushyFlavorPreference;
            SlushiesConsumed = slushiesConsumed;
        }

        public override string? ToString()
        {
            return $"Name: {Name}, Age: {Age}, High Score Rank: {HighScoreRank}, " +
                $"Starting Date: {FirstDate}, Pizzas Consumed: {PizzasConsumed}," +
                $" Bowling High Score: {BowlingHighScore}, " +
                $"Employment Status: {(EmploymentStatus ? "Employed" : "Unemployed")}," +
                $" Slushy Flavor Preference: {SlushyFlavorPreference}, Slushies Consumed: {SlushiesConsumed}";
        }
    }
}
