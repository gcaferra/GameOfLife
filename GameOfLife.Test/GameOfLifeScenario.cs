using Shouldly;
using Xunit;

namespace GameOfLife.Test
{
    public class GameOfLifeScenario
    {
        [Fact]
        public void the_board_8x4_is_generated()
        {
            var sut = new BoardGenerator();

            var board = sut.Generate();
            
            board.Length.ShouldBe(32);
        }

        [Fact]
        public void BoardGenerator_return_a_Board_class()
        {
            var sut = new BoardGenerator();

            sut.Generate().ShouldBeOfType<Board>();
        }

        [Fact]
        public void the_Board_can_say_if_a_Cell_is_Alive_or_Not()
        {
            var sut = new Board(new[,]{{false, true}});

            sut.IsAlive(0,0).ShouldBe(false);
            sut.IsAlive(0,1).ShouldBe(true);
        }
    }

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
            return _board[row, column];
        }
    }


    
    public class BoardGenerator
    {
        public Board Generate()
        {
            return new Board(new[,]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
            });
        }
    }
}