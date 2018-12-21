using System;
using System.IO;

namespace AI.RPS
{
    public class Game
    {
        public History History { get; set; }
        public Score Score { get; set; }
        public int Round { get; set; }
        public string Name { get; set; }

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

        public GameResult NextRound(Choice playerChoice, Choice aiChoice)
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

            return gameResult;
        }

        public Score FinishGame()
        {
            SaveGame();
            return Score;
        }

        private void SaveGame()
        {
            var fileName = "../../Users/" + Name + ".txt";

            if (File.Exists(fileName))
            {
                string[] allLines;
                allLines = File.ReadAllLines(fileName);

                string[] tokens = allLines[0].Split(' ');
                int wins = 0, draws = 0, loses = 0;
                bool parsedWins = Int32.TryParse(tokens[1], out wins);
                bool parsedDraws = Int32.TryParse(tokens[3], out draws);
                bool parsedLoses = Int32.TryParse(tokens[5], out loses);

                if (!parsedWins)
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", tokens[1]);
                if (!parsedDraws)
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", tokens[3]);
                if (!parsedLoses)
                    Console.WriteLine("Int32.TryParse could not parse '{0}' to an int.\n", tokens[5]);

                allLines[0] = "Wins: " + (Score.PlayerWins + wins) + " Draws: " + (Score.Draws + draws) + " Loses: " + (Score.AiWins + loses);
                File.WriteAllLines(fileName, allLines);
                var file = File.Open(fileName, FileMode.Append);

                using (var stream = new StreamWriter(file))
                {
                    foreach (var round in History.Rounds)
                    {
                        stream.WriteLine(round.PlayerChoice);
                    }
                }
            }
            else
            {
                using (StreamWriter file = new StreamWriter(fileName))
                {
                    file.WriteLine("Wins: " + Score.PlayerWins + " Draws: " + Score.Draws + " Loses: " + Score.AiWins);
                    foreach (var round in History.Rounds)
                    {
                        file.WriteLine(round.PlayerChoice);
                    }
                }
            }
        }
    }
}