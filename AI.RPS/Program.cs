using System;
using AI.RPS.AIs;

namespace AI.RPS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var difficulty = ChooseDifficulty();
            Score result;

            switch(difficulty)
            {
                case "easy":
                    result = GameRunner.Run(new Probability());
                    break;
                case "medium":
                    result = GameRunner.Run(new Mixed());
                    break;
                default:
                    result = GameRunner.Run(new RandomPicker());
                    break;
            }

            Console.Read();
        }

        private static void FancyWrite(string @string)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@string);
            Console.ResetColor();
        }

        public static string ChooseDifficulty()
        {
            while (true)
            {
                Console.WriteLine("Choose the difficulty: ");
                FancyWrite("1. Easy");
                FancyWrite("2. Medium");
                FancyWrite("3. Hard");
                var difficulty = Console.ReadLine();

                switch (difficulty.ToLower())
                {
                    case "1":
                    case "e":
                    case "easy":
                        return "easy";
                    case "2":
                    case "m":
                    case "medium":
                        return "medium";
                    case "3":
                    case "h":
                    case "hard":
                        return "hard";
                    default:
                        Console.WriteLine("Wrong input, try again.");
                        break;

                }
            }
        }
    }
}