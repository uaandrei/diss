using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.Interfaces.Move
{
    public interface IMoveStrategy
    {
        IList<Position> GetMoves(Position position);
        IList<Position> GetAttacks(Position position);
    }

    public static class MoveExtensions
    {
        public static void AddPieceIfPossible(this IList<Position> list, Position current, Position dest, Chess.Business.Interfaces.Piece.IPieceContainer container)
        {
            if (!dest.IsInBounds())
                return;

            if (container[dest] != null && container[current].Color == container[dest].Color)
                return;

            list.Add(dest);
        }
    }
}
