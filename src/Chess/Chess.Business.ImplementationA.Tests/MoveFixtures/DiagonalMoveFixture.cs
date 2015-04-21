using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class DiagonalMoveFixture
    {
        private DiagonalMove _sut;
        private IPiece _piece;
        private IList<IPiece> _pieces;

        public DiagonalMoveFixture()
        {
            _sut = new DiagonalMove();
            _piece = Helper.GetMockedPieceAt(4, 4, PieceColor.Black);
            _pieces = new List<IPiece> { _piece };
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        [InlineData(7, 7)]
        [InlineData(5, 3)]
        [InlineData(6, 2)]
        [InlineData(7, 1)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves(_piece, _pieces);

            // assert
            Assert.Equal(13, moves.Count);
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(2, 6)]
        [InlineData(6, 6)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _pieces.Add(Helper.GetMockedPieceAt(7, 7, PieceColor.Black));
            _pieces.Add(Helper.GetMockedPieceAt(x, y, PieceColor.White));
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(_piece, _pieces);

            // assert
            Assert.Single(attacks, move);
            Assert.DoesNotContain(new Position(7, 7), attacks);
        }
    }
}
