using System;
using AI.RPS.Interfaces;

namespace AI.RPS
{
    public static class GameRunner
    {
        public static Score Run(IArtificialInteligence artificialInteligence, int rounds = 15)
        {
            Console.WriteLine("Enter your name: ");
            var name = Console.ReadLine();
            var game = new Game();
            game.Name = name;

            for (var i = 0; i < rounds; i++)
            {
                var playerChoice = ReadPlayerInput();
                var aiChoice = artificialInteligence.Run(game);
                game.NextRound(playerChoice, aiChoice);
                FancyWrite($"Ai choose {aiChoice.ToString()}");
                var currentScore = game.Score;
                FancyWrite(
                    $"Round {game.Round}\nPlayer: {currentScore.PlayerWins}\tAI: {currentScore.AiWins}\tDraws: {currentScore.Draws}");
            }

            var finalScore = game.FinishGame();
            FancyWrite($"Final score:\nPlayer: {finalScore.PlayerWins}\tAI: {finalScore.AiWins}\tDraws: {finalScore.Draws}");
            return finalScore;
        }

        private static Choice ReadPlayerInput()
        {
            while (true)
            {
                Console.WriteLine("Enter input(R/P/S)");
                var input = Console.ReadLine()?.ToUpper();
                switch (input)
                {
                    case "R":
                        return Choice.Rock;
                    case "P":
                        return Choice.Paper;
                    case "S":
                        return Choice.Scissors;
                    default:
                        Console.WriteLine("Wrong input, try again.");
                        break;
                }
            }
        }

        private static void FancyWrite(string @string)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@string);
            Console.ResetColor();
        }
    }
}