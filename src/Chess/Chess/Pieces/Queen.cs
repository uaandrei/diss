using Chess.Helpers;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class Queen : BasePiece
    {
        public override PieceType Type { get { return PieceType.Queen; } }

        public Queen(Position p, PieceColor color)
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
            positions.AddRange(this.GeneratePositions(matrix, xOffset: -1));
            positions.AddRange(this.GeneratePositions(matrix, xOffset: 1));
            positions.AddRange(this.GeneratePositions(matrix, yOffset: -1));
            positions.AddRange(this.GeneratePositions(matrix, yOffset: 1));
            return positions;
        }
    }
}
