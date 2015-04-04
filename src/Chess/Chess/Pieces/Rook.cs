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
            return PositionCalculator.GetXDistance(_curPosition, p) == 0
                || PositionCalculator.GetYDistance(_curPosition, p) == 0;
        }
    }
}
