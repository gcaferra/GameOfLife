using System.Collections.Generic;
using NSubstitute;
using Shouldly;
using Xunit;

namespace GameOfLife.Test
{
    public class GameOfLifeScenario
    {
        [Fact]
        void the_board_8x4_is_generated()
        {
            var sut = new BoardGenerator();

            var board = sut.Generate();
            
            board.Length.ShouldBe(32);
        }

        [Fact]
        void BoardGenerator_return_a_Board_class()
        {
            var sut = new BoardGenerator();

            sut.Generate().ShouldBeOfType<Board>();
        }

        [Fact]
        void the_Board_can_say_if_a_Cell_is_Alive_or_Not()
        {
            var sut = new Board(new[,]{{false, true}});

            sut.IsAlive(0,0).ShouldBe(false);
            sut.IsAlive(0,1).ShouldBe(true);
        }

        [Theory]
        [MemberData(nameof(AliveNeighboursTestData))]
        void the_Board_can_say_if_a_neighbours_is_Alive(bool[,] testData)
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
        
        [Theory]
        [MemberData(nameof(DiedNeighboursTestData))]
        void the_Board_search_the_neighbour_cell_only(bool[,] testData)
        {
            var sut = new Board(testData);

            sut.HasAliveNeighbour(1,1, 0).ShouldBe(true);
        }

        public static IEnumerable<object[]> DiedNeighboursTestData()
        {
            yield return new object[] {new[,]
            {
                {false, false, false},
                {false, false, false},
                {false, false, false},
            }};
            yield return new object[] {new[,]
            {
                {false, false, false},
                {false, false, false},
                {false, false, false},
                {true, true, false}
            }};
            yield return new object[] {new[,]
            {
                {false, false, false, true},
                {false, false, false, true},
                {false, false, false, true},

            }};
        }

        [Fact]
        void the_GameEngine_uses_the_Board()
        {
            var board = Substitute.For<IBoard>();
            
            var sut = new GameEngine(board);

            sut.NextGeneration();

            board.Received().HasAliveNeighbour(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>());
        }
    }
}