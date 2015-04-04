using Chess.Helpers;

namespace Chess.Pieces
{
    public class Pawn : BasePiece
    {
        public override PieceType Type { get { return PieceType.Pawn; } }
        
        public Pawn(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public override bool CanMove(Position p)
        {
            var dist = PositionCalculator.GetYDistance(_curPosition, p);
            if (_color == PieceColor.White)
                return dist == -1;
            return dist == 1;
        }
    }
}
