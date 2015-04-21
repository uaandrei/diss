using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Business.ImplementationA
{
    public class GameTable : IGameTable
    {
        private List<IPiece> _pieces;
        public IEnumerable<IPiece> Pieces { get { return _pieces; } }

        public void Start()
        {
            var pieceFactory = new PieceFactory();
            _pieces = pieceFactory.GetPieces();
            _pieces.ForEach(p => p.PieceMoving += OnPieceMoving);
        }

        private void InitializeTable()
        {
        }

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            if (_pieces.IsOccupied(newPosition))
            {
                var attackedPiece = _pieces.Single(p => p.CurrentPosition == newPosition);
                _pieces.Remove(attackedPiece);
            }
        }
    }

    public static class PieceExtensions
    {
        public static bool IsFree(this IEnumerable<IPiece> pieces, int x, int y)
        {
            return pieces.IsFree(new Position(x, y));
        }

        public static bool IsFree(this IEnumerable<IPiece> pieces, Position pos)
        {
            return !pieces.Any(p => p.CurrentPosition == pos);
        }

        public static bool IsOccupied(this IEnumerable<IPiece> pieces, int x, int y)
        {
            return pieces.IsOccupied(new Position(x, y));
        }

        public static bool IsOccupied(this IEnumerable<IPiece> pieces, Position pos)
        {
            return !pieces.IsFree(pos);
        }

        public static IPiece GetPiece(this IEnumerable<IPiece> pieces, Position pos)
        {
            return pieces.FirstOrDefault(p => p.CurrentPosition == pos);
        }
    }
}
