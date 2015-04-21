using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class LMove : MoveStrategyBase
    {
        public override IList<Position> GetMoves(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            return GetMoves((c, p) => c.IsFree(p), currentPiece, allPieces);
        }

        protected override List<Position> GenerateAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            return GetMoves((c, p) => !c.IsFree(p), currentPiece, allPieces);
        }

        private List<Position> GetMoves(Func<IEnumerable<IPiece>, Position, bool> condition, IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var positions = new List<Position>();
            var moves = GetOffsets();
            for (int i = 0; i < 8; i++)
            {
                var posibleMove = new Position(currentPiece.CurrentPosition.X + moves[i, 0], currentPiece.CurrentPosition.Y + moves[i, 1]);
                if (posibleMove.IsInBounds() && condition(allPieces, posibleMove))
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
