using Chess.Moves;
using Chess.Pieces;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class LMoveFixture
    {
        private LMove _sut;
        private IPieceContainer _pieceContainer;

        public LMoveFixture()
        {
            _pieceContainer = Helper.GetEmptyContainer();
            _pieceContainer.Add(Helper.GetMockedPieceAt(4, 4, PieceColor.Black));
            _sut = new LMove(_pieceContainer);
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
            var moves = _sut.GetMoves(new Position(4, 4));

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
            _pieceContainer.Add(Helper.GetMockedPieceAt(3, 6, PieceColor.Black));
            _pieceContainer.Add(Helper.GetMockedPieceAt(x, y, PieceColor.White));
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(new Position(4, 4));

            // assert
            Assert.Single(attacks, move);
            Assert.DoesNotContain(new Position(3,6), attacks);
        }
    }
}
