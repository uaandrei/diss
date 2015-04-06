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
            var posibleMove = GetPosibleMove();
            if (matrix[posibleMove.X, posibleMove.Y] == (int)PieceType.Empty)
                positions.Add(posibleMove);
            return positions;
        }

        private Position GetPosibleMove()
        {
            if (_color == PieceColor.Black)
                return new Position(_curPosition.X, _curPosition.Y - 1);
            return new Position(_curPosition.X, _curPosition.Y + 1);
        }
    }
}
