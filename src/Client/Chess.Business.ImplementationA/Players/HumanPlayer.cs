using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using Chess.Infrastructure.Enums;

namespace Chess.Business.ImplementationA.Players
{
    public class HumanPlayer : IPlayer
    {
        public bool IsAutomatic { get { return false; } }
        private IEnumerable<IPiece> _pieces;
        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        private int _moveOrder;
        public int MoveOrder { get { return _moveOrder; } }
        public string Name { get { return string.Format("Player {0}", _moveOrder); } }
        public PieceColor Color { get; private set; }

        public HumanPlayer(IEnumerable<IPiece> list, int moveOrder)
        {
            if (list == null || list.Count() <= 0)
                throw new ArgumentException("list");

            Color = list.First().Color;
            _pieces = list;
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
