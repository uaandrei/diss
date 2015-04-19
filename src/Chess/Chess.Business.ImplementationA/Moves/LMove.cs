using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class LMove : MoveStrategyBase
    {
        public LMove(IPieceContainer container)
            : base(container)
        {
        }

        public override IList<Position> GetMoves(Position position)
        {
            return GetMoves((c, p) => c.IsFree(p), position);
        }

        protected override List<Position> GenerateAttacks(Position position)
        {
            return GetMoves((c, p) => !c.IsFree(p), position);
        }

        private List<Position> GetMoves(Func<IPieceContainer, Position, bool> condition, Position position)
        {
            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posibleMove = new Position(position.X + moves[i, 0], position.Y + moves[i, 1]);
                if (posibleMove.IsInBounds() && condition(_container, posibleMove))
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
