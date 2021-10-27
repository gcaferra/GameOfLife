using System;
using System.Linq;

namespace GameOfLife
{
    public class GameEngine
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
                        _board.SetNextVersionCellStatus(row, column, count is 2 or 3);
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
            _board.Commit();
        }

        public string[] Render()
        {
            var render = new string[_board.Rows];
            
            for (var row = 0; row < _board.Rows; row++)
            {
                var line = string.Empty;
                for (var column = 0; column < _board.Columns; column++)
                {
                    line = string.Concat(line, _board.IsAlive(row, column) ? "*" : ".");
                }
                render[row] = line;
            }

            return render;
        }
    }
}