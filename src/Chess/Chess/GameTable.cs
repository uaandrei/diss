using System.Linq;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using Chess.Exceptions;

namespace Chess
{
    public class GameTable : IEnumerable<IPiece>
    {
        public IPieceContainer PieceContainer { get; private set; }

        public GameTable()
        {
            InitializeTable();
        }

        private void InitializeTable()
        {
            PieceContainer = new PieceContainer(new PieceFactory());
            PieceContainer.ForEach(p => p.PieceMoving += OnPieceMoving);
        }

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            if (PieceContainer.IsOccupied(newPosition))
            {
                var attackedPiece = PieceContainer.Single(p => p.CurrentPosition == newPosition);
                PieceContainer.Remove(attackedPiece);
            }
        }

        public IEnumerator<IPiece> GetEnumerator()
        {
            return PieceContainer.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return PieceContainer.GetEnumerator();
        }
    }
}
