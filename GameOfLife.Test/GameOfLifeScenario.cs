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
            var sut = new BoardGenerator(4,8);

            var board = sut.Generate();
            
            board.Length.ShouldBe(32);
        }

        [Fact]
        void BoardGenerator_return_a_Board_class()
        {
            var sut = new BoardGenerator(1,1);

            sut.Generate().ShouldBeOfType<Board>();
        }

        [Fact]
        void BoardGenerator_create_a_board_based_on_specified_constructor_values()
        {
            var rows = 4;
            var columns = 15;
            var sut = new BoardGenerator(rows,columns);

            var board = sut.Generate();
            
            board.Columns.ShouldBe(columns);
            board.Rows.ShouldBe(rows);
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

            sut.HasAliveNeighbour(0,0).ShouldBe(1);
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

            sut.HasAliveNeighbour(1,1).ShouldBe(0);
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
        void Border_cell_should_have_three_neighbours()
        {
            var board = new Board(new[,]
            {
                {false, false, true},
                {false, true, true},
                {false, false, false},
            });
            
            
            board.HasAliveNeighbour(0,2).ShouldBe(2);

            
        }

        [Fact]
        void Liveness_of_Cell_is_asked()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(1);
            board.Columns.Returns(1);
            
            var sut = new GameEngine(board);

            sut.NextGeneration();

            board.Received().IsAlive(Arg.Any<int>(),Arg.Any<int>());
        }

        [Fact]
        void IsAlive_is_Called_for_each_Cell_in_the_Board()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(3);
            board.Columns.Returns(3);
            
            var sut = new GameEngine(board);

            sut.NextGeneration();

            board.Received(9).IsAlive(Arg.Any<int>(),Arg.Any<int>());
        }

        [Fact]
        void for_a_Live_cell_all_Neighbours_are_checked()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(1);
            board.Columns.Returns(1);
            board.IsAlive(0, 0).Returns(true);
            
            var sut = new GameEngine(board);

            sut.NextGeneration();

            board.Received(1).HasAliveNeighbour(0, 0);
        }

        [Fact]
        void for_all_Live_cell_all_Neighbours_are_checked()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(3);
            board.Columns.Returns(3);
            board.IsAlive(0, 0).ReturnsForAnyArgs(true);
            
            var sut = new GameEngine(board);

            sut.NextGeneration();

            board.Received(9).HasAliveNeighbour(Arg.Any<int>(), Arg.Any<int>());
        }

        [Fact]
        void A_new_cell_status_is_set()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(1);
            board.Columns.Returns(1);
            board.IsAlive(0, 0).Returns(true);
            board.HasAliveNeighbour(0, 0).Returns(0);
            var sut = new GameEngine(board);

            sut.NextGeneration();

            board.Received().SetNextVersionCellStatus(0,0, false);

        }

        [Fact]
        void Any_live_cell_with_fewer_than_two_live_neighbours_dies()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(1);
            board.Columns.Returns(1);
            board.IsAlive(0, 0).Returns(true);
            board.HasAliveNeighbour(0, 0).Returns(1);
            var sut = new GameEngine(board);

            sut.NextGeneration();

            board.Received().SetNextVersionCellStatus(0,0, false);

        }

        [Fact]
        void Any_live_cell_with_more_than_three_live_neighbours_dies()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(1);
            board.Columns.Returns(1);
            board.IsAlive(0, 0).Returns(true);
            board.HasAliveNeighbour(0, 0).Returns(4);
            var sut = new GameEngine(board);

            sut.NextGeneration();
            
            board.Received().SetNextVersionCellStatus(0,0, false);

        }

        [Fact]
        void Any_live_cell_with_two_or_three_live_neighbours_lives()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(2);
            board.Columns.Returns(2);
            board.IsAlive(0, 0).Returns(true);
            board.HasAliveNeighbour(0, 0).Returns(3);
            board.HasAliveNeighbour(1, 1).Returns(2);
            var sut = new GameEngine(board);

            sut.NextGeneration();
            
            board.Received().SetNextVersionCellStatus(0,0, true);

        }

        [Fact]
        void Any_dead_cell_with_exactly_three_live_neighbours_becomes_alive()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(1);
            board.Columns.Returns(1);
            board.IsAlive(0, 0).Returns(false);
            board.HasAliveNeighbour(0, 0).Returns(3);
            var sut = new GameEngine(board);

            sut.NextGeneration();
            
            board.Received().SetNextVersionCellStatus(0,0, true);

        }

        [Fact]
        void After_create_the_new_generation_the_Board_is_committed()
        {
            var board = Substitute.For<IBoard>();
            board.Rows.Returns(1);
            board.Columns.Returns(1);
            board.IsAlive(0, 0).Returns(false);
            board.HasAliveNeighbour(0, 0).Returns(3);
            var sut = new GameEngine(board);

            sut.NextGeneration();
            
            board.Received().Commit();

        }

        [Fact]
        void the_NextGeneration_is_correctly_created_using_all_rules()
        {
            var board = new Board(new[,]
            {
                {false, false, true},
                {false, true, true},
                {false, false, false},
            });
            
            var sut = new GameEngine(board);

            sut.NextGeneration();
            
            board.ShouldSatisfyAllConditions(
                () => board.IsAlive(0, 0).ShouldBeFalse(),
                () => board.IsAlive(0, 1).ShouldBeTrue(),
                () => board.IsAlive(0, 2).ShouldBeTrue(),
                () => board.IsAlive(1, 0).ShouldBeFalse(),
                () => board.IsAlive(1, 1).ShouldBeTrue(),
                () => board.IsAlive(1, 2).ShouldBeTrue(),
                () => board.IsAlive(2, 0).ShouldBeFalse(),
                () => board.IsAlive(2, 1).ShouldBeFalse(),
                () => board.IsAlive(2, 2).ShouldBeFalse()
                 );

        }
    }
}