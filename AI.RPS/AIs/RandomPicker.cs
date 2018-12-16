using System;
using AI.RPS.Interfaces;

namespace AI.RPS.AIs
{
    public class RandomPicker : IArtificialInteligence
    {
        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());

        public Choice Run(Game game)
        {
            return (Choice) Random.Next(3);
        }
    }
}