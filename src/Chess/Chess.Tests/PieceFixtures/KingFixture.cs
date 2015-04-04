using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class KingFixture
    {
        private King _sut;

        public KingFixture()
        {
            _sut = new King(new Position(3, 3), PieceColor.Black);
        }

        [Theory]
        [InlineData(4, 4)]
        [InlineData(2, 2)]
        [InlineData(2, 4)]
        [InlineData(4, 2)]
        public void Should_MoveDiagonalyOnlyOneUnit(int x, int y)
        {
            // arrange
            var diagonalPosition = new Position(x, y);
            var wrongPosition = new Position(x - 2, y + 3);

            // act
            var canMoveToDiagonalPosition = _sut.CanMove(diagonalPosition);
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);

            // assert
            Assert.True(canMoveToDiagonalPosition);
            Assert.False(canMoveToWrongPosition);
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(4, 3)]
        [InlineData(3, 2)]
        [InlineData(3, 4)]
        public void Should_MoveOrthogonalyOnlyOneUnit(int x, int y)
        {
            // arrange
            var orthogonalPosition = new Position(x, y);
            var wrongPosition = new Position(x + 2, y - 2);

            // act
            var canMoveToOrthogonalPosition = _sut.CanMove(orthogonalPosition);
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);

            // assert
            Assert.True(canMoveToOrthogonalPosition);
            Assert.False(canMoveToWrongPosition);
        }

        [Fact]
        public void Should_HavePieceTypeKing()
        {
            // assert
            Assert.Equal(PieceType.King, _sut.Type);
        }
    }
}
