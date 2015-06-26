using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.Interfaces.Piece
{
    public interface IPiece
    {
        event PieceMove PieceMoving;
        bool HasMoved { get; set; }
        PieceColor Color { get; }
        PieceType Type { get; }
        Position CurrentPosition { get; set; }
        int Rank { get; }
        char File { get; }
        IList<Position> GetAvailableMoves(IEnumerable<IPiece> allPieces);
        IList<Position> GetAvailableAttacks(IEnumerable<IPiece> allPieces);
        bool Move(Position p);
    }

    public delegate void PieceMove(IPiece piece, Position position);
    public delegate void RemovePiece(IPiece piece);
}
