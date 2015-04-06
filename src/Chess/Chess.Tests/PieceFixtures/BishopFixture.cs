using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class BishopFixture : BasePieceFixture
    {
        public BishopFixture()
        {
            _sut = new Bishop(new Position(4, 4), PieceColor.Black);
        }

        [Theory]
        [InlineData(6, 6)]
        [InlineData(2, 2)]
        [InlineData(6, 2)]
        [InlineData(2, 6)]
        public void GetAvailableMoves_ShouldReturnDiagonalMoves_WhenSpacesAreNotOccupied(int x, int y)
        {
            // arrange
            var diagonalPosition = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Contains(diagonalPosition, moves);
        }

        [Fact]
        public void GetAvailableMoves_ShouldReturnDiagonalMovesUntilOccupiedSpaces()
        {
            // arrange
            _chessMatrix[2, 2] = (int)PieceType.King;
            _chessMatrix[6, 2] = (int)PieceType.Pawn;

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.DoesNotContain(new Position(2, 2),moves);
            Assert.DoesNotContain(new Position(6, 2),moves);
        }

        [Fact]
        public void Should_HavePieceTypeBishop()
        {
            // assert
            Assert.Equal(PieceType.Bishop, _sut.Type);
        }
    }
}
