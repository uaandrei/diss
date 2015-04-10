using Chess.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class CompositeMoveFixture
    {
        private CompositeMove _sut;
        private int[,] _matrix;

        public CompositeMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new CompositeMove(new DiagonalMove(_matrix), new OrthogonalMove(_matrix));
        }

        [Theory]
        [InlineData(4, 5)][InlineData(4, 6)][InlineData(4, 7)][InlineData(5, 4)][InlineData(6, 4)][InlineData(7, 4)]
        [InlineData(5, 5)][InlineData(6, 6)][InlineData(7, 7)][InlineData(5, 3)][InlineData(6, 2)][InlineData(7, 1)]
        public void GetMoves_ShouldReturnPositions(int x, int y)
        {
            // arrange
            var move = new Position(x, y);

            // act
            var moves = _sut.GetMoves(new Position(4, 4));

            // assert
            Assert.Equal(27, moves.Count);
            Assert.Contains(move, moves);
        }

        [Theory]
        [InlineData(6, 4)][InlineData(4, 2)][InlineData(2, 2)][InlineData(2, 6)][InlineData(6, 6)]
        public void GetAttacks_ShouldReturnPositions(int x, int y)
        {
            // arrange
            _matrix[x, y] = 1;
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks(new Position(4, 4));

            // assert
            Assert.Equal(1, attacks.Count);
            Assert.Contains(move, attacks);
        }
    }
}
