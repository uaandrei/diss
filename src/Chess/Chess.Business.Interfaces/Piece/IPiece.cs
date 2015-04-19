using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.Interfaces.Piece
{
    public interface IPiece
    {
        event PieceMove PieceMoving;
        PieceColor Color { get; }
        PieceType Type { get; }
        Position CurrentPosition { get; }
        IList<Position> GetAvailableMoves();
        IList<Position> GetAvailableAttacks();
        bool Move(Position p);
    }

    public delegate void PieceMove(IPiece piece, Position position);
    public delegate void RemovePiece(IPiece piece);
}
