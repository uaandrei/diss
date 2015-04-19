using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.Interfaces.Move
{
    public interface IMoveStrategy
    {
        IList<Position> GetMoves(Position position);
        IList<Position> GetAttacks(Position position);
    }
}
