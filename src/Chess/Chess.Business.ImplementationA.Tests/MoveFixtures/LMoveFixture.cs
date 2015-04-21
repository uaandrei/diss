using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class LMoveFixture
    {
        private LMove _sut;
        private IPiece _piece;
        private IList<IPiece> _pieces;

        public LMoveFixture()
        {
            _piece = Helper.GetMockedPieceAt(4, 4, PieceColor.Black);
            _pieces = new List<IPiece> { _piece };
            _sut = new LMove();
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(2, 5)]
        [InlineData(3, 2)]
        [InlineData(3, 6)]
        [InlineData(5, 2)]
        [InlineData(5, 6)]
        [InlineData(6, 3)]
        [InlineData(6, 5)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves(_piece, _pieces);

            // assert
            Assert.Equal(8, moves.Count);
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(2, 3)]
        [InlineData(5, 2)]
        [InlineData(6, 3)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _pieces.Add(Helper.GetMockedPieceAt(3, 6, PieceColor.Black));
            _pieces.Add(Helper.GetMockedPieceAt(x, y, PieceColor.White));
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(_piece, _pieces);

            // assert
            Assert.Single(attacks, move);
            Assert.DoesNotContain(new Position(3,6), attacks);
        }
    }
}
