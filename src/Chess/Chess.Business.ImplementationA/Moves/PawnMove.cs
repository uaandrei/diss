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

            var posibleMove = GetPosibleMove(currentPiece);
            if (allPieces.IsFree(posibleMove))
                positions.Add(posibleMove);

            return positions;
        }

        protected override List<Position> GenerateAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var positions = new List<Position>();

            var move = GetPosibleMove(currentPiece);
            var posibleAttack = new Position(move.X - 1, move.Y);
            if (!allPieces.IsFree(posibleAttack))
                positions.Add(posibleAttack);
            posibleAttack = new Position(move.X + 1, move.Y);
            if (!allPieces.IsFree(posibleAttack))
                positions.Add(posibleAttack);
            return positions;
        }

        private Position GetPosibleMove(IPiece pawn)
        {
            var pawnPosition = new Position(pawn.CurrentPosition);
            if (pawn.Color == Infrastructure.Enums.PieceColor.White)
                --pawnPosition.Y;
            else
                ++pawnPosition.Y;
            return pawnPosition;
        }
    }
}
