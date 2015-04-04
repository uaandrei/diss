using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class WhitePawnFixture
    {
        private Pawn _sut;

        public WhitePawnFixture()
        {
            _sut = new Pawn(new Position(0, 0), PieceColor.White);
        }

        [Fact]
        public void Should_MoveOnlyOneUnitForward()//_When_ThereAreEmptySpaces()
        {
            // arrange
            var wrongPosition = new Position(0, 3);
            var goodPosition = new Position(0, 1);

            // act
            var canMoveToWrongPosition = _sut.CanMove(wrongPosition);
            var canMoveToGoodPosition = _sut.CanMove(goodPosition);

            // assert
            Assert.False(canMoveToWrongPosition);
            Assert.True(canMoveToGoodPosition);
        }

        [Fact]
        public void Should_HavePieceTypePawn()
        {
            // assert
            Assert.Equal(PieceType.Pawn, _sut.Type);
        }
    }
}
