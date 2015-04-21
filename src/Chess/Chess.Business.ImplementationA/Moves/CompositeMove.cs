using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class CompositeMove : IMoveStrategy
    {
        private IEnumerable<IMoveStrategy> _moveStrategies;

        public CompositeMove(IMoveStrategy strategy1, IMoveStrategy strategy2)
        {
            _moveStrategies = new[] { strategy1, strategy2 };
        }

        public IList<Position> GetMoves(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var moves = new List<Position>();
            foreach (var item in _moveStrategies)
            {
                moves.AddRange(item.GetMoves(currentPiece, allPieces));
            }
            return moves;
        }

        public IList<Position> GetAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var attacks = new List<Position>();
            foreach (var item in _moveStrategies)
            {
                attacks.AddRange(item.GetAttacks(currentPiece, allPieces));
            }
            return attacks;
        }
    }
}
