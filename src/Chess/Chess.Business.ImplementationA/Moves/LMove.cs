using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class LMove : IMoveStrategy
    {
        private IPieceContainer _container;

        public LMove(IPieceContainer container)
        {
            _container = container;
        }

        public IList<Position> GetMoves(Position position)
        {
            return GetMoves((c, p) => c.IsFree(p), position);
        }

        public IList<Position> GetAttacks(Position position)
        {
            return GetMoves((c, p) => !c.IsFree(p), position);
        }

        private IList<Position> GetMoves(Func<IPieceContainer, Position, bool> condition, Position position)
        {

            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posibleMove = new Position(position.X + moves[i, 0], position.Y + moves[i, 1]);
                if (posibleMove.IsInBounds() && condition(_container, posibleMove))
                    positions.AddPieceIfPossible(position, posibleMove, _container);
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
