using System;
using AI.RPS.AIs;

namespace AI.RPS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var result = GameRunner.Run(new CopyCat());

            
        }
    }
}