using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class LMove : IMoveStrategy
    {
        private Position _position;
        private int[,] _matrix;

        public LMove(int[,] matrix, Position position)
        {
            _matrix = matrix;
            _position = position;
        }

        public IList<Position> GetMoves()
        {
            return GetMoves(value => value == 0);
        }

        public IList<Position> GetAttacks()
        {
            return GetMoves(value => value != 0);
        }

        private IList<Position> GetMoves(Func<int, bool> condition)
        {

            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posibleMove = new Position(_position.X + moves[i, 0], _position.Y + moves[i, 1]);
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
