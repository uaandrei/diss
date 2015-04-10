using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class SquareMove : IMoveStrategy
    {
        private Position _position;
        private int[,] _matrix;

        public SquareMove(int[,] matrix, Position position)
        {
            _position = position;
            _matrix = matrix;
        }

        public IList<Position> GetMoves()
        {
            return GetAvailableMoves(v => v == 0);
        }

        public IList<Position> GetAttacks()
        {
            return GetAvailableMoves(v => v != 0);
        }

        private IList<Position> GetAvailableMoves(Func<int, bool> condition)
        {
            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posiblePosition = new Position(_position.X + moves[i, 0], _position.Y + moves[i, 1]);

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
