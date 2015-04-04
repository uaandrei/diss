using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class RookFixture
    {
        private Rook _sut;

        public RookFixture()
        {
            _sut = new Rook(new Position(4, 4), PieceColor.White);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(4, 7)]
        public void Should_MoveOnlyHorizontalAndVertical(int x, int y)
        {
            // arrange
            var goodPosition = new Position(x, y);
            var wrongPosition = new Position(x - 1, y - 1);

            // act
            var canMoveToGoodPosition = _sut.CanMove(goodPosition);
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);

            // assert
            Assert.True(canMoveToGoodPosition);
            Assert.False(canMoveToWrongPosition);
        }

        [Fact]
        public void Should_HavePieceTypeRook()
        {
            // assert
            Assert.Equal(PieceType.Rook, _sut.Type);
        }
    }
}
