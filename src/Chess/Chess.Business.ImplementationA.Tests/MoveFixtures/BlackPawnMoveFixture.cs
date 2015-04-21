using Chess.Business.ImplementationA.Moves;
using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class BlackPawnMoveFixture
    {
        private PawnMove _sut;
        private IPiece _piece;
        private IList<IPiece> _pieces;

        public BlackPawnMoveFixture()
        {
            _sut = new PawnMove();
            _piece = Helper.GetMockedPieceAt(1, 0);
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
            Assert.Contains(new Position(1, 1), moves);
        }

        [Fact]
        public void GetAttacks_ShouldReturnPositions()
        {
            // arrange
            _pieces.Add(Helper.GetMockedPieceAt(0, 1, PieceColor.White));
            _pieces.Add(Helper.GetMockedPieceAt(2, 1, PieceColor.White));

            // act
            var attacks = _sut.GetAttacks(_piece, _pieces);

            // assert
            Assert.Equal(2, attacks.Count);
            Assert.Contains(new Position(0, 1), attacks);
            Assert.Contains(new Position(2, 1), attacks);
        }
    }
}
