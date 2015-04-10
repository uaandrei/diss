using Chess.Helpers;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Pawn : BasePiece
    {
        public override PieceType Type { get { return PieceType.Pawn; } }

        public Pawn(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public Pawn(int x, int y, PieceColor color)
            : base(x, y, color)
        {
        }

        public override IList<Position> GetAvailableMoves(int[,] matrix)
        {
            throw new System.NotImplementedException();
        }
    }
}
