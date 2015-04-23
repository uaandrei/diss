using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class WhitePawnMoveFixture
    {
        private PawnMove _sut;
        private IPiece _piece;
        private IList<IPiece> _pieces;

        public WhitePawnMoveFixture()
        {
            _sut = new PawnMove();
            _piece = Helper.GetMockedPieceAt(1, 7, PieceColor.White);
            _pieces = new List<IPiece> { _piece };
        }

        [Fact]
        public void GetMoves_ShouldReturnReturnPositions()
        {
            // arrange

            // act
            var moves = _sut.GetMoves(_piece, _pieces);

            // assert
            Assert.Single(moves);
            Assert.Contains(new Position(1, 6), moves);
        }

        [Fact]
        public void GetAttacks_ShouldReturnPositions()
        {
            // arrange
            _pieces.Add(Helper.GetMockedPieceAt(0, 6, PieceColor.White));
            _pieces.Add(Helper.GetMockedPieceAt(2, 6, PieceColor.Black));

            // act
            var attacks = _sut.GetAttacks(_piece, _pieces);

            // assert
            Assert.Single(attacks, new Position(2, 6));
        }
    }
}
