using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Queen : BasePiece
    {
        public override PieceType Type { get { return PieceType.Queen; } }

        public Queen(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public Queen(int x, int y, PieceColor color)
            : base(x, y, color)
        {
        }

        public override IList<Position> GetAvailableMoves(int[,] matrix)
        {
            throw new System.NotImplementedException();
        }
    }
}
