using Chess.Helpers;

namespace Chess.Pieces
{
    public class King : BasePiece
    {
        public override PieceType Type { get { return PieceType.King; } }

        public King(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public override bool CanMove(Position p)
        {
            var xDist = PositionCalculator.GetXDistance(_curPosition, p);
            var yDist = PositionCalculator.GetYDistance(_curPosition, p);
            if (xDist > 1 || yDist > 1)
                return false;

            return PositionCalculator.AreDiagonal(_curPosition, p)
                || PositionCalculator.AreOrthogonal(_curPosition, p);
        }
    }
}
