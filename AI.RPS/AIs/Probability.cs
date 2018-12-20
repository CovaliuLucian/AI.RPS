using System;
using System.Linq;
using AI.RPS.Interfaces;

namespace AI.RPS.AIs
{
    public class Probability : IArtificialInteligence
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());
        private double RockProbability, PaperProbability, ScissorsProbability;
        private int strategy = 0;

        public Choice Run(Game game)
        {
            if (game.Round == 0)
                return (Choice)Random.Next(3);

            GetProbabilities(game);

            return Max();
        }

        public void GetProbabilities(Game game)
        {
            double numberOfRock = 0, numberOfPaper = 0, numberOfscissors = 0, total = 0;

            foreach (var round in game.History.Rounds)
            {
                switch (round.PlayerChoice)
                {
                    case Choice.Scissors:
                        { numberOfscissors++; total++; break; }
                    case Choice.Rock:
                        { numberOfRock++; total++; break; }
                    case Choice.Paper:
                        { numberOfPaper++; total++; break; }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(round.PlayerChoice), round.PlayerChoice, "WTF?!");
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

            if (result == RockProbability)
                return Choice.Paper;

            if (result == PaperProbability)
                return Choice.Scissors;

            return Choice.Rock;
        }
    }
}