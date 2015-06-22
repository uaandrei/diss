using Chess.Business.ImplementationA.Moves;
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
            _piece = Helper.GetMockedPieceAt(1, 7);
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
        public void GetMoves_ShouldMoveTwoPositions()
        {
            // arrange
            _piece = Helper.GetMockedPieceAt(3, 6);
            _pieces.Clear();
            _pieces.Add(_piece);

            // act
            var moves = _sut.GetMoves(_piece, _pieces);

            // assert
            Assert.Equal(2, moves.Count);
            Assert.Contains(new Position(3, 4), moves);
        }

        [Fact]
        public void GetAttacks_ShouldReturnPositions()
        {
            // arrange
            _pieces.Add(Helper.GetMockedPieceAt(0, 6, PieceColor.Black));
            _pieces.Add(Helper.GetMockedPieceAt(2, 6, PieceColor.White));

            // act
            var attacks = _sut.GetAttacks(_piece, _pieces);

            // assert
            Assert.Single(attacks, new Position(2, 6));
        }
    }
}
