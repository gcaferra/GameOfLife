using System;
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
    }

    public class Board : IBoard
    {
        readonly bool[,] _board;
        bool[,] _nextGenerationBoard;

        public Board(bool[,] board)
        {
            _board = board;
            Rows = _board.GetUpperBound(0);
            Columns = _board.GetUpperBound(1);
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
            List<bool> alives = new List<bool>()
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
            if (_nextGenerationBoard == null)
            {
                _nextGenerationBoard = new bool[_board.GetUpperBound(0),_board.GetUpperBound(1)];
                Array.Copy(_board, _nextGenerationBoard, _board.Length);
            }

            _nextGenerationBoard[row, column] = newStatus;
        }
    }
}