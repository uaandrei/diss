using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Pieces
{
    public class ChessPiece : IPiece
    {
        private Position _curPosition;
        private PieceColor _color;
        private PieceType _type;
        private IDictionary<PieceType, IMoveStrategy> _moveStrategies;
        public PieceColor Color { get { return _color; } }
        public Position CurrentPosition { get { return _curPosition; } }
        public PieceType Type { get { return _type; } }
        public event PieceMove PieceMoving;

        public ChessPiece(Position p, PieceColor color, PieceType type)
        {
            _curPosition = p;
            _color = color;
            _type = type;
            SetupMoveStrategies();
        }

        public ChessPiece(int x, int y, PieceColor color, PieceType type)
            : this(new Position(x, y), color, type)
        {
        }

        public bool Move(Position newPosition)
        {
            RaisePieceMovingEvent(newPosition);
            _curPosition.X = newPosition.X;
            _curPosition.Y = newPosition.Y;
            return true;
        }


        public IList<Position> GetAvailableMoves(IEnumerable<IPiece> allPieces)
        {
            return _moveStrategies[_type].GetMoves(this, allPieces);
        }

        public IList<Position> GetAvailableAttacks(IEnumerable<IPiece> allPieces)
        {
            return _moveStrategies[_type].GetAttacks(this, allPieces);
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

        private void SetupMoveStrategies()
        {
            _moveStrategies = new Dictionary<PieceType, IMoveStrategy>();
            _moveStrategies.Add(PieceType.Rook, new OrthogonalMove());
            _moveStrategies.Add(PieceType.Knight, new LMove());
            _moveStrategies.Add(PieceType.Bishop, new DiagonalMove());
            _moveStrategies.Add(PieceType.Queen, new CompositeMove(new OrthogonalMove(), new DiagonalMove()));
            _moveStrategies.Add(PieceType.King, new SquareMove());
            _moveStrategies.Add(PieceType.Pawn, new PawnMove());
        }
    }
}
