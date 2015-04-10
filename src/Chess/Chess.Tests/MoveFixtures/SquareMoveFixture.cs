using Chess.Moves;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class SquareMoveFixture
    {
        private int[,] _matrix;
        private SquareMove _sut;

        public SquareMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new SquareMove(_matrix);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves(new Position(0, 0));

            // assert
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _matrix[x, y] = 1;
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(new Position(0, 0));

            // assert
            Assert.Contains(move, attacks);
        }
    }
}
