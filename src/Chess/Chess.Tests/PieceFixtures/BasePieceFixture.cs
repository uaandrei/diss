using System.Linq;
using Chess.Pieces;
using System.Collections.Generic;
using Xunit;

namespace Chess.Tests.PieceFixtures
{
    public class BasePieceFixture
    {
        protected IPiece _sut;
        protected int[,] _chessMatrix;

        public BasePieceFixture()
        {
            _chessMatrix = Helper.GetEmptyChessMatrix();
        }

        protected void AssertPosition(Position testMove, IList<Position> moves, bool shouldContain)
        {
            Assert.True(moves.Contains(testMove) == shouldContain, string.Format("test move: {0} \n generated moves: {1}", GetString(testMove), moves.Select(s => GetString(s)).Aggregate((s1, s2) => s1 + "; " + s2)));
        }

        private string GetString(Position p)
        {
            return string.Format("X:{0},Y:{1}", p.X, p.Y);
        }

    }
}
