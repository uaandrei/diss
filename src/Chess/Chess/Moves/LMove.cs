using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class LMove : IMoveStrategy
    {
        private int[,] _matrix;

        public LMove(int[,] matrix)
        {
            _matrix = matrix;
        }

        public IList<Position> GetMoves(Position position)
        {
            return GetMoves(value => value == 0, position);
        }

        public IList<Position> GetAttacks(Position position)
        {
            return GetMoves(value => value != 0, position);
        }

        private IList<Position> GetMoves(Func<int, bool> condition, Position position)
        {

            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posibleMove = new Position(position.X + moves[i, 0], position.Y + moves[i, 1]);
                if (posibleMove.IsInBounds() && condition(_matrix[posibleMove.X, posibleMove.Y]))
                    positions.Add(posibleMove);
            }
            return positions;
        }

        private int[,] GetOffsets()
        {
            return new[,] { 
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }, 
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }
            };
        }
    }
}
