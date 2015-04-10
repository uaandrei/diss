using Chess.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chess.Tests.MoveFixtures
{
    public class OrtogonalMoveFixture
    {
        private int[,] _matrix;
        private OrtogonalMove _sut;

        public OrtogonalMoveFixture()
        {
            _matrix = Helper.GetEmptyChessMatrix();
            _sut = new OrtogonalMove(_matrix, new Position(4, 4));
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
            var moves = _sut.GetMoves();

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
            _matrix[x, y] = 1;
            var move = new Position(x, y);

            // act
            var attacks = _sut.GetAttacks();

            // assert
            Assert.Equal(1, attacks.Count);
            Assert.Contains(move, attacks);
        }
    }
}
