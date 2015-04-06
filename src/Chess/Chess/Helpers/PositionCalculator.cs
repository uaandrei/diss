using Chess.Pieces;
using System;
using System.Collections.Generic;

namespace Chess.Helpers
{
    static class PositionCalculator
    {
        public static int GetYDistance(Position p1, Position p2)
        {
            return p1.Y - p2.Y;
        }

        public static int GetXDistance(Position p1, Position p2)
        {
            return p1.X - p2.X;
        }

        public static bool AreDiagonal(Position p1, Position p2)
        {
            var absXDist = Math.Abs(GetXDistance(p1, p2));
            var absYDist = Math.Abs(GetYDistance(p1, p2));
            return absXDist == absYDist;
        }

        public static bool AreOrthogonal(Position p1, Position p2)
        {
            return GetXDistance(p1, p2) == 0
                 || GetYDistance(p1, p2) == 0;
        }

        public static IList<Position> GeneratePositions(this IPiece rook, int[,] matrix, int xOffset = 0, int yOffset = 0)
        {
            var positions = new List<Position>();
            var x = rook.CurrentPosition.X + xOffset;
            var y = rook.CurrentPosition.Y + yOffset;

            while (x >= 0 && x <= 7 && y >= 0 && y <= 7)
            {
                if (matrix[x, y] == (int)PieceType.Empty)
                    positions.Add(new Position(x, y));
                else
                    break;
                x += xOffset;
                y += yOffset;
            }
            return positions;
        }
    }
}
