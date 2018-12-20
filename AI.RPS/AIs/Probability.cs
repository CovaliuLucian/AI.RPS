using System;
using System.IO;
using System.Linq;
using AI.RPS.Interfaces;

namespace AI.RPS.AIs
{
    public class Probability : IArtificialInteligence
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());
        private double RockProbability, PaperProbability, ScissorsProbability;
        private double numberOfRock = 0, numberOfPaper = 0, numberOfscissors = 0, total = 0;

        public Choice Run(Game game)
        {
            if (game.Round == 0)
            {
                if (UserExists(game.Name))
                    GetProbabilitiesFromFile(game);
                return (Choice)Random.Next(3);
            }

            GetProbabilities(game);

            return Max();
        }

        public void GetProbabilities(Game game)
        {
            switch (game.History.Rounds.Last().PlayerChoice)
            {
                case Choice.Scissors:
                    { numberOfscissors++; total++; break; }
                case Choice.Rock:
                    { numberOfRock++; total++; break; }
                case Choice.Paper:
                    { numberOfPaper++; total++; break; }
                default:
                    { total++; break; }
            }

            SetProbabiltyRock(numberOfRock / total);
            SetProbabiltyPaper(numberOfPaper / total);
            SetProbabiltyScissors(numberOfscissors / total);
        }

        public void GetProbabilitiesFromFile(Game game)
        {
            var fileName = "../../Users/" + game.Name + ".txt";
            string[] allLines;
            bool firstLine = true;
            allLines = File.ReadAllLines(fileName);

            foreach (var line in allLines)
            {
                if (firstLine)
                {
                    firstLine = false;
                    continue;
                }

                if (line.Equals("Scissors"))
                {
                    numberOfscissors++;
                    total++;
                }

                if (line.Equals("Rock"))
                {
                    numberOfRock++;
                    total++;
                }

                if (line.Equals("Paper"))
                {
                    numberOfPaper++;
                    total++;
                }
            }

            SetProbabiltyRock(numberOfRock / total);
            SetProbabiltyPaper(numberOfPaper / total);
            SetProbabiltyScissors(numberOfscissors / total);
        }


        public void SetProbabiltyRock(double number)
        {
            RockProbability = number;
        }

        public void SetProbabiltyPaper(double number)
        {
            PaperProbability = number;
        }

        public void SetProbabiltyScissors(double number)
        {
            ScissorsProbability = number;
        }

        public Choice Max()
        {
            double result = Math.Max(RockProbability, Math.Max(PaperProbability, ScissorsProbability));
            /*Console.WriteLine("Nr Rock: " + numberOfRock + " Nr Paper: " + numberOfPaper + " Nr Scissors: " + numberOfscissors + " Total: " + total);
            Console.WriteLine("Rock: " + RockProbability + " Paper: " + PaperProbability + " Scissors: " + ScissorsProbability);*/

            if (result == RockProbability)
                return Choice.Paper;

            if (result == PaperProbability)
                return Choice.Scissors;

            return Choice.Rock;
        }

        public bool UserExists(string name)
        {
            var fileName = "../../Users/" + name + ".txt";

            if (File.Exists(fileName))
                return true;

            return false;
        }
    }
}