using System.Collections.Generic;

namespace Chess.Pieces
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

    public delegate void PieceMove(Chess.Pieces.IPiece piece, Chess.Position position);
    public delegate void RemovePiece(Chess.Pieces.IPiece piece);
}
