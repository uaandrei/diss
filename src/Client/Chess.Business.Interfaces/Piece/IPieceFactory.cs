using System.Collections.Generic;

namespace Chess.Business.Interfaces.Piece
{
    public interface IPieceFactory
    {
        List<IPiece> GetAllPieces();
    }
}
