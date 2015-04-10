using Chess.Moves;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class BlackPawnMoveFixture
    {
        private PawnMove _sut;
        private int[,] _matrix;

        public BlackPawnMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new PawnMove(_matrix, new Position(1, 7), PieceColor.Black);
        }

        [Fact]
        public void GetMoves_ShouldReturnReturnPositions()
        {
            // arrange

            // act
            var moves = _sut.GetMoves();

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
            var attacks = _sut.GetAttacks();

            // assert
            Assert.Equal(2, attacks.Count);
            Assert.Contains(new Position(0, 6), attacks);
            Assert.Contains(new Position(2, 6), attacks);
        }
    }
}
