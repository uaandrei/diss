using Chess.Helpers;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Bishop : BasePiece
    {
        public override PieceType Type { get { return PieceType.Bishop; } }

        public Bishop(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public override IList<Position> GetAvailableMoves(int[,] matrix)
        {
            var positions = new List<Position>();
            positions.AddRange(this.GeneratePositions(matrix, 1, 1));
            positions.AddRange(this.GeneratePositions(matrix, 1, -1));
            positions.AddRange(this.GeneratePositions(matrix, -1, 1));
            positions.AddRange(this.GeneratePositions(matrix, -1, -1));
            return positions;
        }
    }
}
