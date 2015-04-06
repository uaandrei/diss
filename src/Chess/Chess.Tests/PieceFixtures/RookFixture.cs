using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class RookFixture : BasePieceFixture
    {
        public RookFixture()
        {
            _sut = new Rook(new Position(4, 4), PieceColor.White);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(4, 7)]
        public void GetAvailableMoves_ShouldReturnOrthogonalMoves_WhenSpacesAreNotOccupied(int x, int y)
        {
            // arrange
            var orthogonalPosition = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Contains(orthogonalPosition, moves);
        }

        [Fact]
        public void GetAvailableMoves_ShouldReturnOrthogonalMovesUntilOccupiedSpaces()
        {
            // arrange
            _chessMatrix[1, 4] = (int)PieceType.King;
            _chessMatrix[4, 7] = (int)PieceType.Pawn;

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.DoesNotContain(new Position(1, 4), moves);
            Assert.DoesNotContain(new Position(4, 7), moves);
        }

        [Fact]
        public void Should_HavePieceTypeRook()
        {
            // assert
            Assert.Equal(PieceType.Rook, _sut.Type);
        }
    }
}
