using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class WhitePawnMoveFixture
    {
        private WhitePawnMove _sut;
        private IPieceContainer _pieceContainer;

        public WhitePawnMoveFixture()
        {
            _pieceContainer = Helper.GetEmptyContainer();
            _pieceContainer.Add(Helper.GetMockedPieceAt(1, 7, PieceColor.White));
            _sut = new WhitePawnMove(_pieceContainer);
        }

        [Fact]
        public void GetMoves_ShouldReturnReturnPositions()
        {
            // arrange

            // act
            var moves = _sut.GetMoves(new Position(1, 7));

            // assert
            Assert.Single(moves);
            Assert.Contains(new Position(1, 6), moves);
        }

        [Fact]
        public void GetAttacks_ShouldReturnPositions()
        {
            // arrange
            _pieceContainer.Add(Helper.GetMockedPieceAt(0, 6, PieceColor.White));
            _pieceContainer.Add(Helper.GetMockedPieceAt(2, 6, PieceColor.Black));

            // act
            var attacks = _sut.GetAttacks(new Position(1, 7));

            // assert
            Assert.Single(attacks, new Position(2, 6));
        }
    }
}
