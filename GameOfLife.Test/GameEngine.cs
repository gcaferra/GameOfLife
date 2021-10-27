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
                    if (_board.IsAlive(row, column))
                    {
                        var count = _board.HasAliveNeighbour(row, column);
                        if (count is 2 or 3)
                        {
                            _board.SetNextVersionCellStatus(row, column, true);
                        }
                        _board.SetNextVersionCellStatus(row, column, false);
                    }
                    else
                    {
                        if (_board.HasAliveNeighbour(row, column) == 3)
                        {
                            _board.SetNextVersionCellStatus(row, column, true);
                        }
                    }
                }
            }
        }
    }
}