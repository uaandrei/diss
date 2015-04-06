using Chess.Pieces;

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
    }
}
