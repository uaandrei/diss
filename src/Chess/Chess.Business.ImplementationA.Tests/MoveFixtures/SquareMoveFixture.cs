using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class SquareMoveFixture
    {
        private SquareMove _sut;
        private IPieceContainer _pieceContainer;

        public SquareMoveFixture()
        {
            _pieceContainer = Helper.GetEmptyContainer();
            _pieceContainer.Add(Helper.GetMockedPieceAt(0, 0, PieceColor.Black));
            _sut = new SquareMove(_pieceContainer);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves(new Position(0, 0));

            // assert
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _pieceContainer.Add(Helper.GetMockedPieceAt(1, 1, PieceColor.Black));
            _pieceContainer.Add(Helper.GetMockedPieceAt(x, y, PieceColor.White));
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(new Position(0, 0));

            // assert
            Assert.Contains(move, attacks);
            Assert.DoesNotContain(new Position(1,1), attacks);
        }
    }
}
