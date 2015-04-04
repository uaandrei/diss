using Chess.Helpers;
using System;

namespace Chess.Pieces
{
    public class Knight : BasePiece
    {
        public Knight(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public override PieceType Type { get { return PieceType.Knight; } }

        public override bool CanMove(Position p)
        {
            var xDist = Math.Abs(PositionCalculator.GetXDistance(_curPosition, p));
            var yDist = Math.Abs(PositionCalculator.GetYDistance(_curPosition, p));

            return (xDist == 2 && yDist == 1)
                || (xDist == 1 && yDist == 2);
        }
    }
}
