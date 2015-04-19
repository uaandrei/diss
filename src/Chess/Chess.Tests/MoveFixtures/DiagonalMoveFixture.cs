using Chess.Moves;
using Chess.Pieces;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class DiagonalMoveFixture
    {
        private DiagonalMove _sut;
        private IPieceContainer _pieceContainer;

        public DiagonalMoveFixture()
        {
            _pieceContainer = Helper.GetEmptyContainer();
            _pieceContainer.Add(Helper.GetMockedPieceAt(4, 4, PieceColor.Black));
            _sut = new DiagonalMove(_pieceContainer);
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
            var moves = _sut.GetMoves(new Position(4, 4));

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
            _pieceContainer.Add(Helper.GetMockedPieceAt(7, 7, PieceColor.Black));
            _pieceContainer.Add(Helper.GetMockedPieceAt(x, y, PieceColor.White));
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(new Position(4, 4));

            // assert
            Assert.Single(attacks, move);
            Assert.DoesNotContain(new Position(7, 7), attacks);
        }
    }
}
