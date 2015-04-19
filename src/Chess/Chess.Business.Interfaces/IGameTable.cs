using Chess.Business.Interfaces.Piece;
using System.Collections.Generic;

namespace Chess.Business.Interfaces
{
    public interface IGameTable : IEnumerable<IPiece>
    {
    }
}
