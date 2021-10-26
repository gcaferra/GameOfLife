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
    }

    public class BoardGenerator
    {
        public bool[,] Generate()
        {
            return new[,]
            {
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
                {false, false, false, false, false, false, false, false},
            };
        }
    }
}