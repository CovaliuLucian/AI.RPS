using System;
using System.Linq;
using AI.RPS.Interfaces;

namespace AI.RPS.AIs
{
    public class CopyCat : IArtificialInteligence
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());
        public Choice Run(Game game)
        {
            if(game.Round == 0)
                return (Choice)Random.Next(3);

            return game.History.Rounds.Last().PlayerChoice;
        }
    }
}