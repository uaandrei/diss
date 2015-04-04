using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class BlackPawnFixture
    {
        private Pawn _sut;

        public BlackPawnFixture()
        {
            _sut = new Pawn(new Position(0, 7), PieceColor.Black);
        }

        [Fact]
        public void Should_MoveOnlyOneUnitForward()//_When_ThereAreEmptySpaces()
        {
            // arrange
            var wrongDest = new Position(0, 5);
            var goodDest = new Position(0, 6);

            // act
            var canMoveToWrongDest = _sut.CanMove(wrongDest);
            var canMoveToGoodDest = _sut.CanMove(goodDest);

            // assert
            Assert.False(canMoveToWrongDest);
            Assert.True(canMoveToGoodDest);
        }

        [Fact]
        public void Should_HavePieceTypePawn()
        {
            // assert
            Assert.Equal(PieceType.Pawn, _sut.Type);
        }
    }
}
