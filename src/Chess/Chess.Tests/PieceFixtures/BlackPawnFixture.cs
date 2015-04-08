using Chess.Pieces;
using Xunit;
using Chess;


namespace Chess.Tests.PieceFixtures
{
    public class BlackPawnFixture : BasePieceFixture
    {
        public BlackPawnFixture()
        {
            _sut = new Pawn(new Position(1, 6), PieceColor.Black);
            _chessMatrix = Helper.GetEmptyChessMatrix();
        }

        [Theory]
        [InlineData(0, 5, true)]
        [InlineData(1, 5, false)]
        public void GetAvailableMoves_ShouldContainAttackMove(int x, int y, bool shouldContain)
        {
            // arrange
            _chessMatrix[1, 5] = (int)PieceType.Queen;
            _chessMatrix[0, 5] = (int)PieceType.Queen;
            var testMove = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            AssertPosition(testMove, moves, shouldContain);
        }

        [Theory]
        [InlineData(1, 5, true)]
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
