using System.Collections.Generic;

namespace Chess.Moves
{
    public interface IMoveStrategy
    {
        IList<Position> GetMoves(Position position);
        IList<Position> GetAttacks(Position position);
    }
}
