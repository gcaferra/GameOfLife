using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Test
{
    public interface IBoard
    {
        int Length { get; }
        int Rows { get; }
        int Columns { get;  }
        bool IsAlive(int row, int column);
        int HasAliveNeighbour(int row, int column);
        void SetNextVersionCellStatus(int row, int column, bool newStatus);
        void Commit();
    }

    public class Board : IBoard
    {
        readonly bool[,] _board;
        readonly bool[,] _next;

        public Board(bool[,] board)
        {
            _board = board;
            Rows = _board.GetUpperBound(0) + 1;
            Columns = _board.GetUpperBound(1) + 1;
            _next = new bool[Rows, Columns];
        }

        public int Length => _board.Length;
        public int Rows { get; }
        public int Columns { get; }

        public bool IsAlive(int row, int column)
        {
            if (row < 0) return false;
            if (column < 0) return false;
            if (row > _board.GetUpperBound(0)) return false;
            if (column > _board.GetUpperBound(1)) return false;

            return _board[row, column];
        }

        public int HasAliveNeighbour(int row, int column)
        {
            List<bool> alives = new List<bool>
            {
                IsAlive(row - 1, column - 1),
                IsAlive(row  - 1, column),
                IsAlive(row - 1, column + 1),
                IsAlive(row  , column - 1),
                IsAlive(row  , column + 1),
                IsAlive(row + 1 , column - 1),
                IsAlive(row + 1 , column),
                IsAlive(row + 1 , column + 1)
            };
            return alives.Count(x => x);
        }

        public void SetNextVersionCellStatus(int row, int column, bool newStatus)
        {
            _next[row, column] = newStatus;
        }

        public void Commit()
        {
            CopyArray(_next,_board);
        }

        static void CopyArray(bool[,] source, bool[,] destination)
        {
            for (var row = 0; row < source.GetUpperBound(0) + 1; row++)
            {
                for (var column = 0; column < source.GetUpperBound(1) + 1; column++)
                {
                    destination[row, column] = source[row, column] ;
                }
            }
        }
    }
}