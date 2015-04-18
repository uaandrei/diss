using System.Collections.Generic;

namespace Chess.Pieces
{
    public class PieceContainer : IPieceContainer
    {
        private IList<IPiece> _pieces;
        public IEnumerable<IPiece> Pieces { get { return _pieces; } }

        public PieceContainer(IList<IPiece> pieces)
        {
            _pieces = pieces;
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
