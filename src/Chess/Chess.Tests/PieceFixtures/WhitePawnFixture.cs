using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class WhitePawnFixture : BasePieceFixture
    {
        public WhitePawnFixture()
        {
            _sut = new Pawn(new Position(1, 1), PieceColor.White);
        }

        [Theory]
        [InlineData(0, 2, true)]
        [InlineData(1, 2, false)]
        public void GetAvailableMoves_ShouldContainAttackMove(int x, int y, bool shouldContain)
        {
            // arrange
            _chessMatrix[1, 2] = (int)PieceType.Queen;
            _chessMatrix[0, 2] = (int)PieceType.Queen;
            var testMove = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            AssertPosition(testMove, moves, shouldContain);
        }

        [Theory]
        [InlineData(1, 2, true)]
        public void GetAvailableMoves(int x, int y, bool shouldContain)
        {
            // arrange
            var testMove = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            AssertPosition(testMove, moves, shouldContain);
        }

        [Fact]
        public void Should_HavePieceTypePawn()
        {
            // assert
            Assert.Equal(PieceType.Pawn, _sut.Type);
        }
    }
}
