using Chess.Helpers;

namespace Chess.Pieces
{
    public class Bishop : BasePiece
    {
        public override PieceType Type { get { return PieceType.Bishop; } }

        public Bishop(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public override bool CanMove(Position p)
        {
            return PositionCalculator.AreOnSameDiagonal(_curPosition, p);
        }
    }
}
