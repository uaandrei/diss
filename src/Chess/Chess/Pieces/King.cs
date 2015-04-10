using Chess.Helpers;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class King : BasePiece
    {
        public override PieceType Type { get { return PieceType.King; } }

        public King(Position p, PieceColor color)
            : base(p, color)
        {
        }

        public King(int x, int y, PieceColor color)
            : base(x, y, color)
        {
        }

        public override IList<Position> GetAvailableMoves(int[,] matrix)
        {
            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var x = _curPosition.X + moves[i, 0];
                var y = _curPosition.Y + moves[i, 1];

                if (x >= 0 && y >= 0 && x <= 7 && y <= 7)
                    positions.Add(new Position(x, y));
                if (matrix[x, y] != (int)PieceType.Empty)
                    break;
            }
            return positions;
        }

        private int[,] GetOffsets()
        {
            return new[,] { 
                { 1, 0 }, { 1, 1 }, { 1, -1 }, 
                { -1, 0 }, { -1, 1 }, { -1, -1 },
                { 0, 1 }, { 0, -1}
            };
        }
    }
}
