using System.Collections.Generic;
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

        [Theory]
        [MemberData(nameof(AliveNeighboursTestData))]
        public void the_Board_can_say_if_a_neighbours_is_Alive(bool[,] testData)
        {
            var sut = new Board(testData);

            sut.HasAliveNeighbour(0,0, 1).ShouldBe(true);
        }

        public static IEnumerable<object[]> AliveNeighboursTestData()
        {
            yield return new object[] {new[,]{{false, true}}};
            yield return new object[] {new[,]
            {
                {false, false},
                {true, false}
            }};
            yield return new object[] {new[,]
            {
                {false, false},
                {false, true}
            }};
        }
    }
}