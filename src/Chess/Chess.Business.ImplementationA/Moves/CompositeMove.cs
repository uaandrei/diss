using Chess.Business.Interfaces.Move;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class CompositeMove : IMoveStrategy
    {
        private IEnumerable<IMoveStrategy> _moveStrategies;

        public CompositeMove(IMoveStrategy strategy1, IMoveStrategy strategy2)
        {
            _moveStrategies = new [] {strategy1, strategy2};
        }

        public IList<Position> GetMoves(Position position)
        {
            var moves = new List<Position>();
            foreach (var item in _moveStrategies)
            {
                moves.AddRange(item.GetMoves(position));
            }
            return moves;
        }

        public IList<Position> GetAttacks(Position position)
        {
            var attacks = new List<Position>();
            foreach (var item in _moveStrategies)
            {
                attacks.AddRange(item.GetAttacks(position));
            }
            return attacks;
        }
    }
}
