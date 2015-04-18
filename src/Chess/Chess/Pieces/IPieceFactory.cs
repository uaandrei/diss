using System.Collections.Generic;

namespace Chess.Pieces
{
    public interface IPieceFactory
    {
        IList<IPiece> Pieces { get; }
        void Initialize(IPieceContainer pieceContainer);
    }
}
