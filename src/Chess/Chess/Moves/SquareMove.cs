using Chess.Pieces;
using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class SquareMove : IMoveStrategy
    {
        private IPieceContainer _container;

        public SquareMove(IPieceContainer container)
        {
            _container = container;
        }

        public IList<Position> GetMoves(Position position)
        {
            return GetAvailableMoves((c, p) => c.IsFree(p), position);
        }

        public IList<Position> GetAttacks(Position position)
        {
            return GetAvailableMoves((c, p) => !c.IsFree(p), position);
        }

        private IList<Position> GetAvailableMoves(Func<IPieceContainer, Position, bool> condition, Position position)
        {
            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posiblePosition = new Position(position.X + moves[i, 0], position.Y + moves[i, 1]);

                if (condition(_container, posiblePosition))
                    positions.AddPieceIfPossible(position, posiblePosition, _container);
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
