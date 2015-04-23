using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA
{
    public class HumanPlayer : IPlayer
    {
        public bool IsAutomatic { get { return false; } }
        public string RequestURI { get { return null; } }
        private IEnumerable<IPiece> _pieces;
        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        private int _moveOrder;
        public int MoveOrder { get { return _moveOrder; } }
        public string Name { get { return string.Format("Player {0}", _moveOrder); } }

        public HumanPlayer(IEnumerable<IPiece> pieces, int moveOrder)
        {
            _pieces = pieces;
            _moveOrder = moveOrder;
        }

        public void Move(Position from, Position to)
        {
            var pieceToMove = _pieces.Single(p => p.CurrentPosition == from);
            pieceToMove.Move(to);
        }

        public bool OwnsPiece(IPiece piece)
        {
            return _pieces.Any(p => p == piece);
        }


        public void Act(IGameTable g) { }
    }
}
