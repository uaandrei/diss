using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Chess.Tests.ChessPieces
{
    public class ChessPieceFixture
    {
        private ChessPiece _sut;

        public ChessPieceFixture()
        {
            _sut = new ChessPiece(new Position(0, 0), PieceColor.Black, PieceType.Bishop);
        }

        [Fact]
        public void Move_ShouldSetCurrentPosition()
        {
            // arrange
            var newPosition = new Position(3, 4);

            // act 
            _sut.Move(newPosition);

            // assert
            Assert.Equal(newPosition, _sut.CurrentPosition);
        }

        [Fact]
        public void GetMoves_GetAttacks_Verify()
        {
            // arrange

            // act
            var moves =_sut.GetAvailableMoves(new List<IPiece>());
            var attacks = _sut.GetAvailableAttacks(new List<IPiece>());

            // assert
            Assert.NotNull(moves);
            Assert.NotNull(attacks);
        }
    }
}
