using System.Collections.Generic;

namespace Chess.Moves
{
    public class CompositeMove : IMoveStrategy
    {
        private IMoveStrategy[] _moveStrategies;

        public CompositeMove(params IMoveStrategy[] moveStrategies)
        {
            _moveStrategies = moveStrategies;
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
