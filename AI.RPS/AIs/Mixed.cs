using System;
using System.IO;
using System.Linq;
using AI.RPS.Interfaces;

namespace AI.RPS.AIs
{
    public class Mixed : IArtificialInteligence
    {
        private RandomPicker Random = new RandomPicker();
        private Probability Probability = new Probability();
        private static readonly Random Rand = new Random(Guid.NewGuid().GetHashCode());

        public Choice Run(Game game)
        {
            if (game.Round == 0)
            {
                Probability.Run(game);
                return Random.Run(game);
            }

            switch(Rand.Next(2))
            {
                case 0:
                    { return Probability.Run(game); }
                default:
                    { Probability.Run(game); return Random.Run(game); }
            }
            
        }
    }
}