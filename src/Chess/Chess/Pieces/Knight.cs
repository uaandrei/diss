using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Knight : BasePiece
    {
        public override PieceType Type { get { return PieceType.Knight; } }

        public Knight(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public Knight(int x, int y, PieceColor color)
            : base(x, y, color)
        {
        }

        public override IList<Position> GetAvailableMoves(int[,] matrix)
        {
            throw new System.NotImplementedException();
        }
    }
}
