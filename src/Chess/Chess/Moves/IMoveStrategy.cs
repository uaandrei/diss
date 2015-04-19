using Chess.Pieces;
using System.Collections.Generic;

namespace Chess.Moves
{
    public interface IMoveStrategy
    {
        IList<Position> GetMoves(Position position);
        IList<Position> GetAttacks(Position position);
    }

    internal static class MoveExtensions
    {
        public static void AddPieceIfPossible(this IList<Position> list, Position current, Position dest, IPieceContainer container)
        {
            if (!dest.IsInBounds())
                return;

            if (container[dest] != null && container[current].Color == container[dest].Color)
                return;

            list.Add(dest);
        }
    }
}
