using System.Collections.Generic;

namespace Chess.Business.Interfaces.Piece
{
    public interface IPieceFactory
    {
        IList<IPiece> Pieces { get; }
        void Initialize(IPieceContainer pieceContainer);
    }
}
