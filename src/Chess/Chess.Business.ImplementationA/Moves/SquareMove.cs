using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class SquareMove : MoveStrategyBase
    {
        public SquareMove(IPieceContainer container)
            : base(container)
        {
        }

        public override IList<Position> GetMoves(Position position)
        {
            return GetAvailableMoves((c, p) => c.IsFree(p), position);
        }

        protected override List<Position> GenerateAttacks(Position position)
        {
            return GetAvailableMoves((c, p) => !c.IsFree(p), position);
        }

        private List<Position> GetAvailableMoves(Func<IPieceContainer, Position, bool> condition, Position position)
        {
            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posiblePosition = new Position(position.X + moves[i, 0], position.Y + moves[i, 1]);

                if (condition(_container, posiblePosition))
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
