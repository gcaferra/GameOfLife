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
            for (var row = 0; row < _board.Rows; row++)
            {
                for (var column = 0; column < _board.Columns; column++)
                {
                    _board.IsAlive(0, 0);
                }
            }

            
            
            _board.HasAliveNeighbour(0, 0);
        }
    }
}