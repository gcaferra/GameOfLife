﻿using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Test
{
    public class Board
    {
        readonly bool[,] _board;

        public Board(bool[,] board)
        {
            _board = board;
        }

        public int Length => _board.Length;

        public bool IsAlive(int row, int column)
        {
            if (row < 0) return false;
            if (column < 0) return false;
            if (row > _board.GetUpperBound(0)) return false;
            if (column > _board.GetUpperBound(1)) return false;

            return _board[row, column];
        }

        public bool HasAliveNeighbour(int row, int column, int count)
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
            return alives.Any();
        }
    }
}