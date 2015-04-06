using System.Collections.Generic;

namespace Chess.Pieces
{
    public interface IPiece
    {
        PieceType Type { get; }
        PieceColor Color { get; }
        Position CurrentPosition { get; }
        IList<Position> GetAvailableMoves(int[,] matrix);
        bool Move(Position p);
    }
}
