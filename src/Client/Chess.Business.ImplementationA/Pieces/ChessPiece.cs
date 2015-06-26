using System.Linq;
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
        public Position CurrentPosition { get { return _curPosition; } set { _curPosition = value; } }
        public PieceType Type { get { return _type; } }
        public int Rank { get { return _curPosition.Rank; } }
        public char File { get { return _curPosition.File; } }
        public bool HasMoved { get; set; }
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

        public ChessPiece(int rank, char file, PieceColor color, PieceType type)
            : this(new Position(rank, file), color, type)
        {
        }

        public bool Move(Position newPosition)
        {
            RaisePieceMovingEvent(newPosition);
            _curPosition.X = newPosition.X;
            _curPosition.Y = newPosition.Y;
            HasMoved = true;
            return true;
        }


        public IList<Position> GetAvailableMoves(IEnumerable<IPiece> allPieces)
        {
            var moves = _moveStrategies[_type].GetMoves(this, allPieces);
            if (_type == PieceType.King)
                AddCastlingMoves(moves, allPieces);
            return moves;
        }

        public IList<Position> GetAvailableAttacks(IEnumerable<IPiece> allPieces)
        {
            return _moveStrategies[_type].GetAttacks(this, allPieces);
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", _color, _type, _curPosition);
        }

        private void AddCastlingMoves(IList<Position> moves, IEnumerable<IPiece> allPieces)
        {
            var king = this;
            if (king.HasMoved)
                return;

            var isB1Empty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "b1");
            var isC1Empty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "c1");
            var isD1Empty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "d1");
            var isF1Empty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "f1");
            var isG1Empty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "g1");
            var kRook = allPieces.FirstOrDefault(p => p.CurrentPosition.ToAlgebraic() == "a1");
            var qRook = allPieces.FirstOrDefault(p => p.CurrentPosition.ToAlgebraic() == "h1");
            if (isB1Empty && isC1Empty && isD1Empty && kRook != null && !kRook.HasMoved)
                moves.Add(new Position("c1"));
            if (isF1Empty && isG1Empty && qRook != null && !qRook.HasMoved)
                moves.Add(new Position("g1"));
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
