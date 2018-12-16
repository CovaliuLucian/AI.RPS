namespace AI.RPS
{
    public class GameRound
    {
        /// <summary>
        /// From the player perspective
        /// </summary>
        public GameResult Result { get; set; }

        public Choice PlayerChoice { get; set; }
        public Choice AiChoice { get; set; }
        public int RoundCount { get; set; }
    }
}