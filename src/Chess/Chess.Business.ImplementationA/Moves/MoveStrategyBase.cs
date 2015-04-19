using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public abstract class MoveStrategyBase : IMoveStrategy
    {
        protected IPieceContainer _container;

        public MoveStrategyBase(IPieceContainer pieceContainer)
        {
            _container = pieceContainer;
        }

        public abstract IList<Position> GetMoves(Position position);

        public IList<Position> GetAttacks(Position position)
        {
            var attacks = GenerateAttacks(position);
            return FilterOutInvalidAttacks(attacks, position);
        }

        protected abstract List<Position> GenerateAttacks(Position position);

        private bool IsAttackInvalid(Position current, Position dest)
        {
            return dest.IsInBounds() && _container[dest] != null && _container[current].Color == _container[dest].Color;
        }

        protected IList<Position> FilterOutInvalidAttacks(List<Position> positions, Position currentPosition)
        {
            positions.RemoveAll(p => IsAttackInvalid(currentPosition, p));
            return positions;
        }
    }
}
