using Chess.Business.ImplementationA.Moves;
using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;
using Xunit;

namespace Chess.Tests.ChessPieces
{
    public class ChessPieceMoveIntegrationFixture
    {
        private ChessPiece _sut;
        private IPiece _piece;
        private IList<IPiece> _pieces;

        public ChessPieceMoveIntegrationFixture()
        {
            _piece = Helper.GetMockedPieceAt(0, 0, PieceColor.Black);
            _pieces = new List<IPiece> { _piece };
            _sut = new ChessPiece(new Position(), PieceColor.Black, PieceType.Knight);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void KnightTypePiece_ShouldReturn_LMoves(int x, int y)
        {
            // arrange
            var moveStrategy = new LMove();
            var move = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves(_pieces);

            // assert
            Assert.Contains(move, moves);
        }

        [Fact]
        public void Type_ShouldBeKnight()
        {
            // assert
            Assert.Equal(PieceType.Knight, _sut.Type);
        }
    }
}
