using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class KnightFixture
    {
        private Knight _sut;

        public KnightFixture()
        {
            _sut = new Knight(new Position(3, 3), PieceColor.Black);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 4)]
        [InlineData(5, 2)]
        [InlineData(5, 4)]
        [InlineData(2, 5)]
        [InlineData(4, 5)]
        [InlineData(4, 1)]
        [InlineData(2, 1)]
        public void Should_MoveInL(int x, int y)
        {
            // arrange
            var LPosition = new Position(x, y);
            var wrongPosition = new Position(x - 2, y - 2);

            // act
            var canMoveToLPosition = _sut.CanMove(LPosition);
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);

            // assert
            Assert.True(canMoveToLPosition);
            Assert.False(canMoveToWrongPosition);
        }

        [Fact]
        public void Should_HavePieceTypeKnight()
        {
            // assert
            Assert.Equal(PieceType.Knight, _sut.Type);
        }
    }
}
