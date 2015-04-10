using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Rook : BasePiece
    {
        public override PieceType Type { get { return PieceType.Rook; } }

        public Rook(Position position, PieceColor color)
            : base(position, color)
        {
        }

        public Rook(int x, int y, PieceColor color)
            : base(x, y, color)
        {
        }

        public override IList<Position> GetAvailableMoves(int[,] matrix)
        {
            return GetOrthogonalMoves(matrix);
        }

        private IList<Position> GetOrthogonalMoves(int[,] matrix)
        {
            throw new System.NotImplementedException();
        }
    }
}
