namespace Chess.Pieces
{
    public interface IPiece
    {
        PieceType Type { get; }
        PieceColor Color { get; }
        bool CanMove(Position p);
        bool Move(Position p);
    }
}
