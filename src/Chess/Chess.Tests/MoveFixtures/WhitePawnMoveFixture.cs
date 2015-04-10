using Chess.Moves;
using Chess.Pieces;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class WhitePawnMoveFixture
    {
        private WhitePawnMove _sut;
        private int[,] _matrix;

        public WhitePawnMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new WhitePawnMove(_matrix);
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
            _matrix[0, 1] = 1;
            _matrix[2, 1] = 1;

            // act
            var attacks = _sut.GetAttacks(new Position(1, 0));

            // assert
            Assert.Equal(2, attacks.Count);
            Assert.Contains(new Position(0, 1), attacks);
            Assert.Contains(new Position(2, 1), attacks);
        }
    }
}
