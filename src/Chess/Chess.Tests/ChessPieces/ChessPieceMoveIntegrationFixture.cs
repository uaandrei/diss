using Chess.Moves;
using Chess.Pieces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chess.Tests.ChessPieces
{
    public class ChessPieceMoveIntegrationFixture
    {
        private ChessPiece _sut;
        private IPieceContainer _pieceContainer;

        public ChessPieceMoveIntegrationFixture()
        {
            _pieceContainer = Helper.GetEmptyContainer();
            _pieceContainer.Add(Helper.GetMockedPieceAt(0, 0, PieceColor.Black));
            var moveStrategy = new LMove(_pieceContainer);
            _sut = new ChessPiece(new Position(), PieceColor.Black, PieceType.Knight, moveStrategy);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void KnightTypePiece_ShouldReturn_LMoves(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetAvailableMoves();

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
