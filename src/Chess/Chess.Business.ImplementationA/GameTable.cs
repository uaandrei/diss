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
        private IPieceContainer _pieceContainer;

        public GameTable()
        {
            InitializeTable();
        }

        private void InitializeTable()
        {
            _pieceContainer = new PieceContainer(new PieceFactory());
            _pieceContainer.ForEach(p => p.PieceMoving += OnPieceMoving);
        }

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            if (_pieceContainer.IsOccupied(newPosition))
            {
                var attackedPiece = _pieceContainer.Single(p => p.CurrentPosition == newPosition);
                _pieceContainer.Remove(attackedPiece);
            }
        }

        public IEnumerator<IPiece> GetEnumerator()
        {
            return _pieceContainer.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _pieceContainer.GetEnumerator();
        }
    }
}
