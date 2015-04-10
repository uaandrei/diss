using Chess.Moves;
using Chess.Pieces;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class WhitePawnMoveFixture
    {
        private IMoveStrategy _sut;
        private int[,] _matrix;

        public WhitePawnMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new PawnMove(_matrix, new Position(1, 0), PieceColor.White);
        }

        [Fact]
        public void GetMoves_ShouldReturnReturnPositions()
        {
            // arrange

            // act
            var moves = _sut.GetMoves();

            // assert
            Assert.Equal(1, moves.Count);
            Assert.Contains(new Position(1, 1), moves);
        }

        [Fact]
        public void GetAttacks_ShouldReturnPositions()
        {
            // arrange
            _matrix[0, 1] = 1;
            _matrix[2, 1] = 1;

            // act
            var attacks = _sut.GetAttacks();

            // assert
            Assert.Contains(new Position(0, 1), attacks);
            Assert.Contains(new Position(2, 1), attacks);
        }
    }
}
