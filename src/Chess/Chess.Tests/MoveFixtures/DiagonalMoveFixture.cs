using Chess.Moves;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class DiagonalMoveFixture
    {
        private int[,] _matrix;
        private DiagonalMove _sut;

        public DiagonalMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new DiagonalMove(_matrix);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        [InlineData(7, 7)]
        [InlineData(5, 3)]
        [InlineData(6, 2)]
        [InlineData(7, 1)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves(new Position(4, 4));

            // assert
            Assert.Equal(13, moves.Count);
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(2, 6)]
        [InlineData(6, 6)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _matrix[x, y] = 1;
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(new Position(4, 4));

            // assert
            Assert.Single(attacks);
            Assert.Contains(move, attacks);
        }
    }
}
