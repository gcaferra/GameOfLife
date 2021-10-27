using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameEngine = new GameEngine(new BoardGenerator(10, 10).Generate());
            
            Console.WriteLine("Press Any key to exit...");
            
            while (true)
            {
                while(!Console.KeyAvailable)
                {
                    gameEngine.NextGeneration();
                    foreach (var line in gameEngine.Render())
                    {
                        Console.WriteLine(line);   
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine();
                }
                
                var k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.Escape)
                    break;

            }
            
            
        }
    }
}