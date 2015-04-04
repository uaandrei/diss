using Chess.Helpers;

namespace Chess.Pieces
{
    public class Rook : BasePiece
    {
        public override PieceType Type { get { return PieceType.Rook; } }

        public Rook(Position position, PieceColor color)
            : base(position, color)
        {
        }

        public override bool CanMove(Position p)
        {
            return PositionCalculator.AreOrthogonal(_curPosition, p);
        }
    }
}
