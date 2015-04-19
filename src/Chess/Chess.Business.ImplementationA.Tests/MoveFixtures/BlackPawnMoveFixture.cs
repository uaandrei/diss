using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class BlackPawnMoveFixture
    {
        private BlackPawnMove _sut;
        private IPieceContainer _pieceContainer;

        public BlackPawnMoveFixture()
        {
            _pieceContainer = Helper.GetEmptyContainer();
            _pieceContainer.Add(Helper.GetMockedPieceAt(1, 0, PieceColor.Black));
            _sut = new BlackPawnMove(_pieceContainer);
        }

        [Fact]
        public void GetMoves_ShouldReturnReturnPositions()
        {
            // arrange

            // act
            var moves = _sut.GetMoves(new Position(1, 0));

            // assert
            Assert.Single(moves);
            Assert.Contains(new Position(1, 1), moves);
        }

        [Fact]
        public void GetAttacks_ShouldReturnPositions()
        {
            // arrange
            _pieceContainer.Add(Helper.GetMockedPieceAt(0, 1, PieceColor.White));
            _pieceContainer.Add(Helper.GetMockedPieceAt(2, 1, PieceColor.White));

            // act
            var attacks = _sut.GetAttacks(new Position(1, 0));

            // assert
            Assert.Equal(2, attacks.Count);
            Assert.Contains(new Position(0, 1), attacks);
            Assert.Contains(new Position(2, 1), attacks);
        }
    }
}
