using Chess.Pieces;
using System;
using System.Collections.Generic;

namespace Chess.Helpers
{
    static class PositionCalculator
    {
        public static IList<Position> GeneratePositions(this IPiece rook, int[,] matrix, int xOffset = 0, int yOffset = 0)
        {
            // TODO: figure out how to distinguish attack from move| offset list? perhaps refactor into a class Move ==>> strategy pattern
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
