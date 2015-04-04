namespace Chess.Pieces
{
    public abstract class BasePiece : IPiece
    {
        protected Position _curPosition;
        protected PieceColor _color;
        public PieceColor Color { get { return _color; } }
        public Position CurrentPosition { get { return _curPosition; } }
        public abstract PieceType Type { get; }

        public BasePiece(Position p, PieceColor color)
        {
            _curPosition = p;
            _color = color;
        }

        public bool Move(Position newPosition)
        {
            if (!CanMove(newPosition))
                return false;
            _curPosition = newPosition;
            return true;
        }

        public abstract bool CanMove(Position p);
    }
}
