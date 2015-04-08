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

        public override IList<Position> GetAvailableMoves(int[,] matrix)
        {
            var positions = new List<Position>();
            
            var yDirection = GetYDirection();
            var x = _curPosition.X;
            var y = _curPosition.Y + yDirection;
            if (matrix[x, y] == (int)PieceType.Empty)
                positions.Add(x, y);
            if (matrix[x - 1, y] != (int)PieceType.Empty)
                positions.Add(x - 1, y);
            if (matrix[x + 1, y] != (int)PieceType.Empty)
                positions.Add(x + 1, y);
            
            return positions;
        }

        private int GetYDirection()
        {
            return _color == PieceColor.Black ? -1 : 1;
        }
    }
}
