using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.Interfaces.Move
{
    public interface IMoveStrategy
    {
        IList<Position> GetMoves(IPiece currentPiece, IEnumerable<IPiece> allPieces);
        IList<Position> GetAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces);
    }
}
