using System;

namespace GameOfLife.Test
{
    public class BoardGenerator
    {
        readonly int _rows;
        readonly int _columns;

        public BoardGenerator(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public Board Generate()
        {
            return new Board(GenerateBoard());
        }
        bool[,] GenerateBoard()
        {
            var board = new bool[_rows, _columns];
            var random = new Random(DateTime.Now.Millisecond);

            for (var row = 0; row < _rows; row++)
            {
                for (var column = 0; column < _columns; column++)
                {
                    board[row, column] = random.NextBoolean();
                }
            }

            return board;
        }
    }
}