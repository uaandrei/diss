using Chess.Moves;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class BlackPawnMoveFixture
    {
        private BlackPawnMove _sut;
        private int[,] _matrix;

        public BlackPawnMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new BlackPawnMove(_matrix);
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
            _matrix[0, 6] = 1;
            _matrix[2, 6] = 1;

            // act
            var attacks = _sut.GetAttacks(new Position(1, 7));

            // assert
            Assert.Equal(2, attacks.Count);
            Assert.Contains(new Position(0, 6), attacks);
            Assert.Contains(new Position(2, 6), attacks);
        }
    }
}
