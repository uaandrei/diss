using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class KnightFixture : BasePieceFixture
    {
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
        public void GetAvailableMoves_ShouldReturnLMoves_WhenSpacesAreNotOccupied(int x, int y)
        {
            // arrange
            var LPosition = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Contains(LPosition, moves);
        }

        [Fact]
        public void GetAvailableMoves_ShouldReturnDiagonalMovesUntilOccupiedSpaces()
        {
            // arrange
            _chessMatrix[5, 2] = (int)PieceType.King;
            _chessMatrix[4, 1] = (int)PieceType.Pawn;

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.DoesNotContain(new Position(5, 2),moves);
            Assert.DoesNotContain(new Position(4, 1),moves);
        }

        [Fact]
        public void Should_HavePieceTypeKnight()
        {
            // assert
            Assert.Equal(PieceType.Knight, _sut.Type);
        }
    }
}
