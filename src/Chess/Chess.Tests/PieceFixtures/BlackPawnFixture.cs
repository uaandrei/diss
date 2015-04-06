using Chess.Pieces;
using Xunit;
using Chess;


namespace Chess.Tests.PieceFixtures
{
    public class BlackPawnFixture
    {
        private Pawn _sut;
        private int[,] _chessMatrix;

        public BlackPawnFixture()
        {
            _sut = new Pawn(new Position(1, 6), PieceColor.Black);
            _chessMatrix = Helper.GetEmptyChessMatrix();
        }

        [Fact]
        public void GetAvaibleMoves_Should_ReturnOneUnitForwardPosition_WhenSpaceNotOccupied()
        {
            // arrange
            var forwardPosition = new Position(1, 5);

            // act
            var moves = _sut.GetAvailableMoves(_chessMatrix);

            // assert
            Assert.Equal(1, moves.Count);
            Assert.Contains(forwardPosition, moves);
        }

        [Fact]
        public void GetAvaibleMoves_Should_ReturnNoPosition_WhenSpaceIsOccupied()
        {
            // arrange
            _chessMatrix[1, 5] = (int)PieceType.Rook;

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
