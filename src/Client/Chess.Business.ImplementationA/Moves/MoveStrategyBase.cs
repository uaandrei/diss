using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public abstract class MoveStrategyBase : IMoveStrategy
    {
        public abstract IList<Position> GetMoves(IPiece currentPiece, IEnumerable<IPiece> allPieces);

        public IList<Position> GetAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var attacks = GenerateAttacks(currentPiece, allPieces);
            return FilterOutInvalidAttacks(attacks, currentPiece, allPieces);
        }

        protected abstract List<Position> GenerateAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces);

        private bool IsAttackValid(Position current, Position dest, IEnumerable<IPiece> pieces)
        {
            return dest.IsInBounds() && pieces.GetPiece(dest) != null && pieces.GetPiece(current).Color != pieces.GetPiece(dest).Color;
        }

        protected IList<Position> FilterOutInvalidAttacks(List<Position> positions, IPiece currentPiece, IEnumerable<IPiece> pieces)
        {
            positions.RemoveAll(p => !IsAttackValid(currentPiece.CurrentPosition, p, pieces));
            return positions;
        }
    }
}
