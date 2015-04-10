using System.Collections.Generic;

namespace Chess.Moves
{
    public interface IMoveStrategy
    {
        IList<Position> GetMoves();
        IList<Position> GetAttacks();
    }
}
