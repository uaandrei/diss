using Xunit;
using Chess.Pieces;

namespace Chess.Tests.PieceFixtures
{
    public class QueenFixture
    {
        private Queen _sut;

        public QueenFixture()
        {
            _sut = new Queen(new Position(4, 4), PieceColor.Black);
        }

        [Theory]
        [InlineData(6, 6)]
        [InlineData(2, 2)]
        [InlineData(6, 2)]
        [InlineData(2, 6)]
        public void Should_MoveDiagonaly(int x, int y)
        {
            // arrange
            var diagonalPosition = new Position(x, y);
            var wrongPosition = new Position(7, 3);

            // act
            var canMoveToDiagonalPosition = _sut.CanMove(diagonalPosition);
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);

            // assert
            Assert.True(canMoveToDiagonalPosition);
            Assert.False(canMoveToWrongPosition);
        }

        [Theory]
        [InlineData(6, 4)]
        [InlineData(2, 4)]
        [InlineData(4, 2)]
        [InlineData(4, 6)]
        public void Should_MoveOrthogonaly(int x, int y)
        {
            // arrange
            var orthogonalPosition = new Position(x, y);
            var wrongPosition = new Position(7, 3);

            // act
            var canMoveToOrthogonalPosition = _sut.CanMove(orthogonalPosition);
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);

            // assert
            Assert.True(canMoveToOrthogonalPosition);
            Assert.False(canMoveToWrongPosition);
        }

        [Fact]
        public void Should_HavePieceTypeQueen()
        {
            // assert
            Assert.Equal(PieceType.Queen, _sut.Type);
        }
    }
}
