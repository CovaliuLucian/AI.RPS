using System;

namespace AI.RPS
{
    public class Game
    {
        public History History { get; set; }
        public Score Score { get; set; }
        public int Round { get; set; }

        public Game()
        {
            NewGame();
        }

        public void NewGame()
        {
            History = new History();
            Score = new Score();
            Round = 0;
        }

        private GameResult ChooseWinner(Choice playerChoice, Choice aiChoice)
        {
            if (playerChoice == aiChoice)
                return GameResult.Draw;
            switch (playerChoice)
            {
                case Choice.Scissors:
                    return aiChoice == Choice.Paper ? GameResult.Win : GameResult.Lose;
                case Choice.Rock:
                    return aiChoice == Choice.Scissors ? GameResult.Win : GameResult.Lose;
                case Choice.Paper:
                    return aiChoice == Choice.Rock ? GameResult.Win : GameResult.Lose;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerChoice), playerChoice, "WTF?!");
            }
        }

        public void NextRound(Choice playerChoice, Choice aiChoice)
        {
            var gameResult = ChooseWinner(playerChoice, aiChoice);

            var currentRound = new GameRound
            {
                PlayerChoice = playerChoice,
                AiChoice = aiChoice,
                Result = gameResult,
                RoundCount = ++Round
            };

            switch (gameResult)
            {
                case GameResult.Win:
                    Score.PlayerWins++;
                    break;
                case GameResult.Lose:
                    Score.AiWins++;
                    break;
                case GameResult.Draw:
                    Score.Draws++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            History.Add(currentRound);
        }

        public Score FinishGame()
        {
            SaveGame();
            return Score;
        }

        private void SaveGame()
        {
            //TODO
        }
    }
}