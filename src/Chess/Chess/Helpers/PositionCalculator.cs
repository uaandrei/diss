using System;

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

        public static bool AreOnSameDiagonal(Position p1, Position p2)
        {
            var absXDist = Math.Abs(GetXDistance(p1, p2));
            var absYDist = Math.Abs(GetYDistance(p1, p2));
            return absXDist == absYDist;
        }
    }
}
