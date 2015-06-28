using System.Linq;
using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.PubSubEvents;
using FenService.Interfaces;

namespace Chess.Business.ImplementationA.Pieces
{
    public class ChessPiece : IPiece
    {
        private Position _curPosition;
        private PieceColor _color;
        private IDictionary<PieceType, IMoveStrategy> _moveStrategies;
        public PieceColor Color { get { return _color; } }
        public Position CurrentPosition { get { return _curPosition; } set { _curPosition = value; } }
        public PieceType Type { get; set; }
        public int Rank { get { return _curPosition.Rank; } }
        public char File { get { return _curPosition.File; } }
        public bool HasMoved { get; set; }
        public event PieceMove PieceMoving;

        public ChessPiece(Position p, PieceColor color, PieceType type)
        {
            _curPosition = p;
            _color = color;
            Type = type;
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
            var moves = _moveStrategies[Type].GetMoves(this, allPieces);
            if (Type == PieceType.King)
                AddCastlingMoves(moves, allPieces);
            return moves;
        }

        public IList<Position> GetAvailableAttacks(IEnumerable<IPiece> allPieces)
        {
            return _moveStrategies[Type].GetAttacks(this, allPieces);
        }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}", _color, Type, _curPosition);
        }

        private void AddCastlingMoves(IList<Position> moves, IEnumerable<IPiece> allPieces)
        {
            var king = this;
            if (king.HasMoved)
                return;

            var kingRank = king.CurrentPosition.Rank;
            var isBEmpty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "b" + kingRank);
            var isCEmpty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "c" + kingRank);
            var isDEmpty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "d" + kingRank);
            var isFEmpty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "f" + kingRank);
            var isGEmpty = !allPieces.Any(p => p.CurrentPosition.ToAlgebraic() == "g" + kingRank);
            var kRook = allPieces.FirstOrDefault(p => p.CurrentPosition.ToAlgebraic() == "a" + kingRank);
            var qRook = allPieces.FirstOrDefault(p => p.CurrentPosition.ToAlgebraic() == "h" + kingRank);
            if (isBEmpty && isCEmpty && isDEmpty && kRook != null && !kRook.HasMoved)
                moves.Add(new Position("c" + kingRank));
            if (isFEmpty && isGEmpty && qRook != null && !qRook.HasMoved)
                moves.Add(new Position("g" + kingRank));
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

    public static class ConvertExtensions
    {
        public static ChessPiece ToPiece(this PieceInfo pieceInfo)
        {
            return new ChessPiece(pieceInfo.Rank, pieceInfo.File, pieceInfo.Color, pieceInfo.Type);
        }
        public static PieceInfo GetInfo(this IPiece p)
        {
            return new PieceInfo
                {
                    Color = p.Color,
                    File = p.File,
                    Rank = p.Rank,
                    Type = p.Type
                };
        }
    }
}
