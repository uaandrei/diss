using System.Collections.Generic;

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

        public BasePiece(int x, int y, PieceColor color)
            : this(new Position(x, x), color)
        {
        }

        public bool Move(Position newPosition)
        {
            throw new System.NotImplementedException();
        }

        public abstract IList<Position> GetAvailableMoves(int[,] matrix);
    }
}
