using System.Collections.Generic;

namespace AI.RPS
{
    public class History
    {
        public IList<GameRound> Rounds = new List<GameRound>();

        public void Add(GameRound round)
        {
            Rounds.Add(round);
        }
    }
}