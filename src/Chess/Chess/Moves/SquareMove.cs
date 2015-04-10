using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class SquareMove : IMoveStrategy
    {
        private int[,] _matrix;

        public SquareMove(int[,] matrix)
        {
            _matrix = matrix;
        }

        public IList<Position> GetMoves(Position position)
        {
            return GetAvailableMoves(v => v == 0,position);
        }

        public IList<Position> GetAttacks(Position position)
        {
            return GetAvailableMoves(v => v != 0,position);
        }

        private IList<Position> GetAvailableMoves(Func<int, bool> condition,Position position)
        {
            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posiblePosition = new Position(position.X + moves[i, 0], position.Y + moves[i, 1]);

                if (posiblePosition.IsInBounds() && condition(_matrix[posiblePosition.X, posiblePosition.Y]))
                    positions.Add(posiblePosition);
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
