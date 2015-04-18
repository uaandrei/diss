using Chess.Moves;
using System.Collections.Generic;

namespace Chess.Pieces
{
    public class ChessPiece : IPiece
    {
        private Position _curPosition;
        private PieceColor _color;
        private PieceType _type;
        private IMoveStrategy _moveStrategy;
        public PieceColor Color { get { return _color; } }
        public Position CurrentPosition { get { return _curPosition; } }
        public PieceType Type { get { return _type; } }
        public event PieceMove PieceMoving;

        public ChessPiece(Position p, PieceColor color, PieceType type, IMoveStrategy moveStrategy)
        {
            _curPosition = p;
            _color = color;
            _type = type;
            _moveStrategy = moveStrategy;
        }

        public ChessPiece(int x, int y, PieceColor color, PieceType type, IMoveStrategy moveStrategy)
            : this(new Position(x, y), color, type, moveStrategy)
        {
        }

        public bool Move(Position newPosition)
        {
            RaisePieceMovingEvent(newPosition);
            _curPosition.X = newPosition.X;
            _curPosition.Y = newPosition.Y;
            return true;
        }


        public IList<Position> GetAvailableMoves()
        {
            return _moveStrategy.GetMoves(_curPosition);
        }

        public IList<Position> GetAvailableAttacks()
        {
            return _moveStrategy.GetAttacks(_curPosition);
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", _color, _type, _curPosition);
        }

        private void RaisePieceMovingEvent(Position newPosition)
        {
            var eventHandler = PieceMoving;
            if (eventHandler != null)
                eventHandler(this, newPosition);
        }
    }
}
