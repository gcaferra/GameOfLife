using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static int _origRow;
        static int _origCol;

        static void Main(string[] args)
        {
            var gameEngine = new GameEngine(new BoardGenerator(20, 20).Generate());
            
            Console.WriteLine("Press ESC to stop");

            _origRow = Console.CursorTop + 1;
            _origCol = Console.CursorLeft;

            do
            {
                while (!Console.KeyAvailable)
                {
                    gameEngine.NextGeneration();
                    RenderNewGeneration(gameEngine.Render());
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);


        }

        static void RenderNewGeneration(string[] strings)
        {
            Console.SetCursorPosition(_origCol, _origRow);
            foreach (var line in strings)
            {
                Console.WriteLine(line);
            }
        }
    }
}