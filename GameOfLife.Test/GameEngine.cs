namespace GameOfLife.Test
{
    internal class GameEngine
    {
        readonly IBoard _board;

        public GameEngine(IBoard board)
        {
            _board = board;
        }

        public void NextGeneration()
        {
            _board.IsAlive(0, 0);
            
            _board.HasAliveNeighbour(0, 0, 0);
        }
    }
}