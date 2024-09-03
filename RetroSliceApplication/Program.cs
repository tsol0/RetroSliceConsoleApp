using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace RetroSliceApplication
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            RunApp();
            //Application by Lerato Mathebe (602243), Lesedi Mhlongo 602183, Manqoba Mbambo (601276), Tsolo Khambule (578682)
        }
        private static void RunApp()
        {
            bool exit = false;
            List<Customer> applicants = [];
            List<Customer> qualified = [];
            List<Customer> unqualified = [];

            Console.WriteLine("Hello Admin :)\nBelow are the services this system provides.");


            int selectedOption;

            InitialDisplay();

            while (exit != true)
            {

                Console.WriteLine("Please select an option");
                try
                {
                    selectedOption = Convert.ToInt32(Console.ReadLine());

                }
                catch (Exception)
                {
                    Console.WriteLine("Enter valid input");
                    continue;
                }

                Menu m = (Menu)selectedOption;

                switch (m)
                {
                    case Menu.Exit:
                        exit = true;
                        Console.WriteLine("Good Bye...");
                        break;
                    case Menu.CaptureDetails:
                        CaptureApplicant(applicants);
                        CleanConsole();

                        break;
                    case Menu.ArcadeBowlingScore:
                        CustomerScores(applicants);
                        CleanConsole();
                        break;
                    case Menu.QualifyForCredit:
                        Thread t = new Thread(() => Qaulify(applicants, qualified, unqualified));
                        t.Start();
                        t.Join();
                        CleanConsole();
                        break;
                    case Menu.AverageNumPizzasConsumed:
                        decimal result = AvgNumPizzasConsumed(applicants);
                        Console.WriteLine("The Average Pizzas Consumed per First Visit: " + result + " pizzas");
                        CleanConsole();
                        break;
                    case Menu.OldestAndYoungestCustomer:
                        Thread n = new Thread(() => OldestToYoungestApplicant(applicants));
                        n.Start();
                        n.Join();
                        CleanConsole();
                        break;
                    case Menu.LifetimeCredits:
                        LifeTimeCreditsList(applicants);
                        CleanConsole();
                        break;
                    case Menu.Help:
                        InitialDisplay();
                        CleanConsole();
                        break;
                    case Menu.SaveToFile:
                        Storage(applicants);
                        break;
                    default:
                        Console.WriteLine("Please select a valid integer");
                        break;

                }

            }
        }

        private static void Storage(List<Customer> applicants)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            foreach (var item in applicants)
            {
                using StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "CustomerList.txt"), true);
                outputFile.WriteLine(item);
            }
        }

        private static void LifeTimeCreditsList(List<Customer> applicants)
        {
            List<Customer> qualifiers = [];
            foreach (var customer in applicants)
            {
                int yearsLapsed = (new DateTime(1, 1, 1) + (DateTime.Now - customer.FirstDate)).Year - 1;
                if (yearsLapsed >= 10) qualifiers.Add(customer);
            }

            Console.WriteLine("List of Lifetime Credit Customers");
            Console.WriteLine("Name\tYears");
            foreach (var customer in qualifiers) 
            {
                int yearsLapsed = (new DateTime(1, 1, 1) + (DateTime.Now - customer.FirstDate)).Year - 1;
                Console.WriteLine($"{customer.Name}\t{yearsLapsed}");
            }

        }

        private static void InitialDisplay()
        {
            Console.WriteLine("======================================================");
            Console.WriteLine("Exit: 0\nCapture Applicant Details: 1\n" +
                    "Arcade and Bowling stats: 2\n" +
                    "Check game token credit qualification: 3\n" +
                    "Calculate the average number of pizzas consumed per first visit: 4\n" +
                    "Oldest to youngest applicant list: 5\n" +
                    "Life time credit award list: 6\n" +
                    "Help: 7\n" +
                    "SaveToFile: 8");
            Console.WriteLine("======================================================");
        }

        private static void CleanConsole()
        {
            Console.WriteLine("Press enter to clear");
            Console.ReadLine();
            Console.Clear();
        }

        enum Menu
        {
            Exit = 0,
            CaptureDetails,
            ArcadeBowlingScore,
            QualifyForCredit,
            AverageNumPizzasConsumed,
            OldestAndYoungestCustomer,
            LifetimeCredits,
            Help,
            SaveToFile,
        }

        private static void OldestToYoungestApplicant(List<Customer> applicants)
        {
            List<Customer> sortedAges = [];
            sortedAges.AddRange(applicants);

            int totalApplicants = sortedAges.Count;
            int r, c;
            Customer temp;
            bool swapped;
            //Utilised bubble sort to sort from biggest to lowest number
            for (r = 0; r < totalApplicants; r++)
            {
                swapped = false;
                for (c = 0; c < totalApplicants - r - 1; c++)
                {
                    if (sortedAges[c].Age > sortedAges[c + 1].Age)
                    {
                        temp = sortedAges[c];
                        sortedAges[c] = sortedAges[c + 1];
                        sortedAges[c + 1] = temp;
                        swapped = true;
                    }

                }
                    if (swapped is false) break;
            }
            Console.WriteLine("============================");
            Console.WriteLine("Youngest to Oldest Customers");
            Console.WriteLine("============================");

            foreach (var item in sortedAges)
            {
                Console.WriteLine($"{item.Name}:\t{item.Age}");
            }
        }

        private static decimal AvgNumPizzasConsumed(List<Customer> applicants)
        {
            int sum = 0;
            int totalApplicants = applicants.Count;
            foreach (var applicant in applicants)
            {
                sum += applicant.PizzasConsumed;
            }
            return (decimal) sum / totalApplicants;
        }

        static void CustomerScores(List<Customer> applicants)
        {
            List<Customer> bowlingRanks = [];  
            List<Customer> arcadeRanks = [];

            bowlingRanks.AddRange(applicants);
            arcadeRanks.AddRange(applicants);

            int totalApplicants = applicants.Count;
            int r, c;
            Customer temp;
            bool swapped;

            Console.WriteLine("======================================================");
            Console.WriteLine("Bowling Rankings");
            Console.WriteLine("======================================================");
            Console.WriteLine("Name\tRank");
            Console.WriteLine("======================================================");

            //Utilised bubble sort to sort from biggest to lowest number
            for (r = 0; r < totalApplicants; r++)
            {
                swapped = false;
                for (c = 0; c < totalApplicants - r - 1; c++)
                {
                    if (bowlingRanks[c].BowlingHighScore < bowlingRanks[c + 1].BowlingHighScore)
                    {
                        temp = bowlingRanks[c];
                        bowlingRanks[c] = bowlingRanks[c + 1];
                        bowlingRanks[c + 1] = temp;
                        swapped = true;
                    }

                }
                    if (swapped is false) break;
            }

            foreach (var item in bowlingRanks)
            {
                Console.WriteLine($"{item.Name}\t{item.BowlingHighScore}");
            }
            Console.WriteLine();

            Console.WriteLine("======================================================");
            Console.WriteLine("Arcade Rankings");
            Console.WriteLine("======================================================");
            Console.WriteLine("Name\tRank");
            Console.WriteLine("======================================================");

            for (r = 0; r < totalApplicants; r++)
            {
                swapped = false;
                for (c = 0; c < totalApplicants - r - 1; c++)
                {
                    if (arcadeRanks[c].HighScoreRank < arcadeRanks[c + 1].HighScoreRank)
                    {
                        temp = arcadeRanks[c];
                        arcadeRanks[c] = arcadeRanks[c + 1];
                        arcadeRanks[c + 1] = temp;
                        swapped = true;
                    }

                }
                    if (swapped is false) break;
            }

            foreach (var item in arcadeRanks)
            {
                Console.WriteLine($"{item.Name}\t{item.HighScoreRank}");
            }

        }

        public static void Qaulify(List<Customer> applicants, List<Customer> qualified, List<Customer> unqualified)
        {

            foreach (var item in applicants)
            {
                int yearsLapsed = (new DateTime(1, 1, 1) + (DateTime.Now - item.FirstDate)).Year - 1;
                int avgScore = (item.HighScoreRank + item.BowlingHighScore) / 2;
                Boolean scoreFlag = false;

                if (item.HighScoreRank > 2000 || item.BowlingHighScore > 1500 || avgScore > 1200)
                {
                    scoreFlag = true;
                }

                if (item.EmploymentStatus == true && 
                    yearsLapsed >= 2 && scoreFlag &&
                    item.PizzasConsumed >= 3 && !string.Equals(item.SlushyFlavorPreference, "Gooey Gulp Galore", StringComparison.OrdinalIgnoreCase) &&
                    item.SlushiesConsumed >= 4)
                {
                    qualified.Add(item);
                }
                unqualified.Add(item);
            }

            Console.WriteLine("The list of people who qualify for credit:");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");
            int QualifiedTotal = qualified.Count;
            foreach(var item in qualified)
            {
                Console.WriteLine($"Name: {item.Name}\tAge: {item.Age}");
            }
            Console.WriteLine($"Total credited = {QualifiedTotal}");

            Console.WriteLine("The list of people who do not qualify for credit:");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");
            int unQualifiedTotal = unqualified.Count;
            foreach (var item in unqualified)
            {
                Console.WriteLine($"Name: {item.Name}\tAge: {item.Age}");
            }
            Console.WriteLine($"Total uncredited = {unQualifiedTotal}");

            qualified.Clear();
            unqualified.Clear();

        }

        public static List<Customer> CaptureApplicant(List<Customer> applicants)
        {
            Boolean addMoreApplicants = true;
            char userInput;
            while (addMoreApplicants)
            {
                Console.WriteLine("Would you like to add an applicant?\nEnter Y (Yes) and N (No)");
                try
                {
                    userInput = Convert.ToChar(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter valid input");
                    continue;
                    //throw;
                }

                if (userInput == 'Y')
                {
                    while (addMoreApplicants != false)
                    {

                        string name;
                        int age;
                        int highScoreRank;
                        DateTime startingDate;
                        string dayFirstVisit;
                        int numPizzasConsumed;
                        int bowlingHighScore;
                        Boolean employmentStatus;
                        string slushyPreference;
                        int slushiesConsumed;



                        Boolean validInput = false;

                        while (validInput != true)
                        {
                            try
                            {
                                Console.WriteLine("Enter applicant name");
                                name = Console.ReadLine();

                                Console.WriteLine("Enter your age");
                                age = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Enter high score rank");
                                highScoreRank = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Enter date of first visit in format yyyy/mm/dd");
                                dayFirstVisit = Console.ReadLine();
                                string[] dateInput = dayFirstVisit.Split("/");
                                startingDate = new DateTime(Convert.ToInt32(dateInput[0]),
                                    Convert.ToInt32(dateInput[1]), Convert.ToInt32(dateInput[2]));

                                Console.WriteLine("Enter number pizzas consumed");
                                numPizzasConsumed = Convert.ToInt32(Console.ReadLine());


                                Console.WriteLine("Enter bowling score rank");
                                bowlingHighScore = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Are you employed?");
                                if (age >= 18)
                                {
                                    Console.WriteLine("You're above 18.\nPlease enter yes or no. If you are employed or not.");
                                }
                                else
                                {
                                    
                                    Console.WriteLine("You're under 18.\nPlease enter yes or no if your parent/parents is/are employed or not.");
                                }

                                employmentStatus = Console.ReadLine() == "yes" ? true : false;

                                string[] availableSlushyFlavours = {"Raspberry", "Cherry", "Watermelon", "Gooey Gulp Galore" };
                                Console.WriteLine("Enter your slushy preference.\n" +
                                    "Slushy Favours:\n" +
                                    "1. Raspberry\n" +
                                    "2. Cherry\n" +
                                    "3. Watermelon\n" +
                                    "4.Gooey Gulp Galore");
                                Boolean validFlavour = false;
                                Console.WriteLine("Enter your preferred slushy from the list above.");
                                slushyPreference = Console.ReadLine();

                                while (validFlavour != true)
                                {
                                    foreach (var item in availableSlushyFlavours)
                                    {
                                        if (string.Equals(slushyPreference, item, StringComparison.OrdinalIgnoreCase))
                                        {
                                            validFlavour = true;
                                            break;
                                        }

                                    }

                                    if (validFlavour is true)
                                    {
                                        break;
                                    }
                                    Console.WriteLine("Enter your preferred slushy from above.");
                                    slushyPreference = Console.ReadLine();

                                }


                                Console.WriteLine("Enter number of slushies you have consumed");
                                slushiesConsumed = Convert.ToInt32(Console.ReadLine());


                                validInput = true;

                                var addApplicant = new Customer(name, age, highScoreRank, startingDate, numPizzasConsumed,
                                    bowlingHighScore, employmentStatus, slushyPreference, slushiesConsumed);

                                applicants.Add(addApplicant);
                                Console.WriteLine("Customer was added");

                                Console.WriteLine("Would you like to add another.Y (Yes) or N (No)");
                                if (Convert.ToChar(Console.ReadLine()) == 'Y')
                                {
                                    continue;
                                }
                                else
                                {
                                    addMoreApplicants = false;
                                    break;
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Please enter correct data into the fields");
                                validInput = false;
                            }

                        }


                    }
                } 
                addMoreApplicants = false;


            }
            return applicants;
        }
    }
}
