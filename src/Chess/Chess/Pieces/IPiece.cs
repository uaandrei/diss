using System.Collections.Generic;

namespace Chess.Pieces
{
    public interface IPiece
    {
        PieceColor Color { get; }
        PieceType Type { get; }
        Position CurrentPosition { get; }
        IList<Position> GetAvailableMoves();
        IList<Position> GetAvailableAttacks();
        bool Move(Position p);
    }
}
