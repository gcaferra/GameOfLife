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
    }

    public class Board
    {
        readonly bool[,] _board;

        public Board(bool[,] board)
        {
            _board = board;
        }

        public int Length => _board.Length;
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