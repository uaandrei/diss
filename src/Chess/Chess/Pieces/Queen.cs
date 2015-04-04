using Chess.Helpers;

namespace Chess.Pieces
{
    public class Queen : BasePiece
    {
        public override PieceType Type { get { return PieceType.Queen; } }

        public Queen(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public override bool CanMove(Position p)
        {
            return PositionCalculator.AreDiagonal(_curPosition, p)
                || PositionCalculator.AreOrthogonal(_curPosition, p);
        }
    }
}
