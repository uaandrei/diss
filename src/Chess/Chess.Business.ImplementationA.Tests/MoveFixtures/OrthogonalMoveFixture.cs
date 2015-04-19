using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class OrthogonalMoveFixture
    {
        private OrthogonalMove _sut;
        private IPieceContainer _pieceContainer;

        public OrthogonalMoveFixture()
        {
            _pieceContainer = Helper.GetEmptyContainer();
            _pieceContainer.Add(Helper.GetMockedPieceAt(4, 4, PieceColor.Black));
            _sut = new OrthogonalMove(_pieceContainer);
        }

        [Theory]
        [InlineData(4, 5)]
        [InlineData(4, 6)]
        [InlineData(4, 7)]
        [InlineData(5, 4)]
        [InlineData(6, 4)]
        [InlineData(7, 4)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves(new Position(4, 4));

            // assert
            Assert.Equal(14, moves.Count);
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(6, 4)]
        [InlineData(4, 2)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _pieceContainer.Add(Helper.GetMockedPieceAt(4, 5, PieceColor.Black));
            _pieceContainer.Add(Helper.GetMockedPieceAt(x, y, PieceColor.White));
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(new Position(4, 4));

            // assert
            Assert.Single(attacks, move);
            Assert.DoesNotContain(new Position(4,5), attacks);
        }
    }
}
