using System.Linq;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using Chess.Exceptions;

namespace Chess
{
    public class GameTable : IEnumerable<IPiece>
    {
        private int[,] _matrix;
        private PieceFactory _factory;
        public IPieceContainer PieceContainer { get; private set; }

        public GameTable()
        {
            _matrix = new int[8, 8];
            _factory = new PieceFactory(_matrix);
            InitializeTable();
        }

        private void InitializeTable()
        {
            _factory.Initialize();
            PieceContainer = new PieceContainer(_factory.Pieces);
            PieceContainer.ForEach(p => p.PieceMoving += OnPieceMoving);
        }

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            if (_matrix[newPosition.X, newPosition.Y] != 0)
            {
                //attack
                var attackedPiece = PieceContainer.Single(p => p.CurrentPosition == newPosition);
                PieceContainer.Remove(attackedPiece);
            }
            _matrix[piece.CurrentPosition.X, piece.CurrentPosition.Y] = 0;
            _matrix[newPosition.X, newPosition.Y] = (int)piece.Type;
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
