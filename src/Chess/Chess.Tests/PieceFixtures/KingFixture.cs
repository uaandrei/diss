using Chess.Pieces;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class KingFixture : BasePieceFixture
    {
        public KingFixture()
        {
            _sut = new King(new Position(3, 3), PieceColor.Black);
        }

        [Theory]
        [InlineData(4, 4)]
        [InlineData(2, 2)]
        [InlineData(2, 4)]
        [InlineData(4, 2)]
        public void Should_MoveDiagonalyOnlyOneUnit(int x, int y)
        {
            // arrange
            var diagonalPosition = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Contains(diagonalPosition, moves);
        }

        [Fact]
        public void GetAvailableMoves_ShouldIncludeOccupiedSpaces()
        {
            // arrange
            _chessMatrix[2, 2] = (int)PieceType.King;

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Contains(new Position(2, 2), moves);
            Assert.DoesNotContain(new Position(3, 3), moves);
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(4, 3)]
        [InlineData(3, 2)]
        [InlineData(3, 4)]
        public void Should_MoveOrthogonalyOnlyOneUnit(int x, int y)
        {
            // arrange
            var orthogonalPosition = new Position(x, y);
            
            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Contains(orthogonalPosition, moves);
        }

        [Fact]
        public void GetAvailableMoves_ShouldIncludeOccupiedSpaces2()
        {
            // arrange
            _chessMatrix[4, 3] = (int)PieceType.King;

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Contains(new Position(4, 3), moves);
            Assert.DoesNotContain(new Position(3, 3), moves);
        }

        [Fact]
        public void Should_HavePieceTypeKing()
        {
            // assert
            Assert.Equal(PieceType.King, _sut.Type);
        }
    }
}
