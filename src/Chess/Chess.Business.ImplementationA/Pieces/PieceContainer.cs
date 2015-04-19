using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Business.ImplementationA.Pieces
{
    public class PieceContainer : IPieceContainer
    {
        private IList<IPiece> _pieces;
        private IPieceFactory _factory;
        public IEnumerable<IPiece> Pieces { get { return _pieces; } }

        public IPiece this[Position pos]
        {
            get
            {
                return _pieces.FirstOrDefault(p => p.CurrentPosition == pos);
            }
        }

        public PieceContainer(IPieceFactory factory)
        {
            _factory = factory;
            _factory.Initialize(this);
            _pieces = _factory.Pieces;
        }

        public void Add(IPiece piece)
        {
            _pieces.Add(piece);
        }

        public void Remove(IPiece piece)
        {
            _pieces.Remove(piece);
        }

        public void ForEach(System.Action<IPiece> action)
        {
            foreach (var piece in _pieces)
            {
                action(piece);
            }
        }

        public bool IsFree(int x, int y)
        {
            return IsFree(new Position(x, y));
        }

        public bool IsFree(Position pos)
        {
            return !Pieces.Any(p => p.CurrentPosition == pos);
        }

        public bool IsOccupied(int x, int y)
        {
            return IsOccupied(new Position(x, y));
        }

        public bool IsOccupied(Position pos)
        {
            return !IsFree(pos);
        }

        public IEnumerator<IPiece> GetEnumerator()
        {
            return _pieces.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _pieces.GetEnumerator();
        }
    }
}
