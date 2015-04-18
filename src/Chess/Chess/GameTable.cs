using System.Linq;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using Chess.Exceptions;

namespace Chess
{
    public class GameTable
    {
        private int[,] _matrix;
        private PieceFactory _factory;
        private List<IPiece> _pieces;
        public IList<IPiece> Pieces { get { return _pieces; } }
        public event RemovePiece PieceAttacked;

        public GameTable()
        {
            _matrix = new int[8, 8];
            _factory = new PieceFactory(_matrix);
            InitializeTable();
        }

        private void InitializeTable()
        {
            _factory.Initialize();
            _pieces = _factory.Pieces;
            foreach (var piece in _pieces)
            {
                piece.PieceMoving += OnPieceMoving;
            }
        }

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            if (_matrix[newPosition.X, newPosition.Y] != 0)
            {
                //attack
                var attackedPiece = _pieces.First(p => p.CurrentPosition == newPosition);
                if (attackedPiece == null)
                    throw new PieceNotFoundException(newPosition);

                Pieces.Remove(attackedPiece);
            }
            _matrix[piece.CurrentPosition.X, piece.CurrentPosition.Y] = 0;
            _matrix[newPosition.X, newPosition.Y] = (int)piece.Type;
        }
    }
}
