using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class BishopFixture
    {
        private Bishop _sut;

        public BishopFixture()
        {
            _sut = new Bishop(new Position(4, 4), PieceColor.Black);
        }

        [Theory]
        [InlineData(6, 6)]
        [InlineData(2, 2)]
        [InlineData(6, 2)]
        [InlineData(2, 6)]
        public void Should_MoveOnlyDiagonaly(int x, int y)
        {
            // arrange
            var diagonalPosition = new Position(x, y);
            var wrongPosition = new Position(x - 2, y - 1);

            // act
            var canMoveToDiagonalPosition = _sut.CanMove(diagonalPosition);
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);

            // assert
            Assert.True(canMoveToDiagonalPosition);
            Assert.False(canMoveToWrongPosition);
        }

        [Fact]
        public void Should_HavePieceTypeBishop()
        {
            // assert
            Assert.Equal(PieceType.Bishop, _sut.Type);
        }
    }
}
