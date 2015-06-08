using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class PawnMove : MoveStrategyBase
    {
        public override IList<Position> GetMoves(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var positions = new List<Position>();

            foreach (var posibleMove in GetPosibleMoves(currentPiece))
            {
                if (posibleMove.IsInBounds() && allPieces.IsFree(posibleMove))
                    positions.Add(posibleMove);
            }
            return positions;
        }

        protected override List<Position> GenerateAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var positions = new List<Position>();

            var move = GetPosibleMoves(currentPiece)[0];
            var posibleAttack = new Position(move.X - 1, move.Y);
            if (posibleAttack.IsInBounds() && !allPieces.IsFree(posibleAttack))
                positions.Add(posibleAttack);
            posibleAttack = new Position(move.X + 1, move.Y);
            if (!allPieces.IsFree(posibleAttack))
                positions.Add(posibleAttack);
            return positions;
        }

        private IList<Position> GetPosibleMoves(IPiece pawn)
        {
            // add test
            var posibleMoves = new List<Position>();
            var pp = new Position(pawn.CurrentPosition);
            if (pawn.Color == Infrastructure.Enums.PieceColor.White)
            {
                posibleMoves.Add(new Position(pp.X, pp.Y + 1));
                if (pawn.CurrentPosition.Rank == 2)
                    posibleMoves.Add(new Position(pp.X, pp.Y + 2));
            }
            else
            {
                posibleMoves.Add(new Position(pp.X, pp.Y - 1));
                if (pawn.CurrentPosition.Rank == 7)
                    posibleMoves.Add(new Position(pp.X, pp.Y - 2));
            }
            return posibleMoves;
        }
    }
}
