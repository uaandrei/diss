using Chess.Moves;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class LMoveFixture
    {
        private int[,] _matrix;
        private LMove _sut;

        public LMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new LMove(_matrix, new Position(4, 4));
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(2, 5)]
        [InlineData(3, 2)]
        [InlineData(3, 6)]
        [InlineData(5, 2)]
        [InlineData(5, 6)]
        [InlineData(6, 3)]
        [InlineData(6, 5)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves();

            // assert
            Assert.Equal(8, moves.Count);
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(5, 2)]
        [InlineData(6, 3)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _matrix[x, y] = 1;
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks();

            // assert
            Assert.Single(attacks);
            Assert.Contains(move, attacks);
        }
    }
}
