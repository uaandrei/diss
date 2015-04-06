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

        [Fact]
        public void GetAvaibleMoves_ShouldReturnOneUnitForwardPosition_WhenSpaceNotOccupied()
        {
            // arrange
            var forwardPosition = new Position(1, 2);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Equal(1, moves.Count);
            Assert.Contains(forwardPosition, moves);
        }

        [Fact]
        public void GetAvaibleMoves_ShouldReturnNoPosition_WhenSpaceIsOccupied()
        {
            // arrange
            _chessMatrix[1, 2] = (int)PieceType.Rook;

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Empty(moves);
        }

        [Fact]
        public void Should_HavePieceTypePawn()
        {
            // assert
            Assert.Equal(PieceType.Pawn, _sut.Type);
        }
    }
}
