using Xunit;
using Chess.Pieces;

namespace Chess.Tests.PieceFixtures
{
    public class QueenFixture : BasePieceFixture
    {
        public QueenFixture()
        {
            _sut = new Queen(new Position(4, 4), PieceColor.Black);
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
            Assert.DoesNotContain(new Position(2, 2), moves);
            Assert.DoesNotContain(new Position(6, 2), moves);
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
        public void Should_HavePieceTypeQueen()
        {
            // assert
            Assert.Equal(PieceType.Queen, _sut.Type);
        }
    }
}
