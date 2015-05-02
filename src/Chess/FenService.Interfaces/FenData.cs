using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenService.Interfaces
{
    public class FenData
    {
        public IPiece[] Pieces { get; set; }
        public PieceColor ColorToMove { get; set; }
        public bool WhiteLeftCastling { get; set; }
        public bool WhiteRightCastling { get; set; }
        public bool BlackLeftCastling { get; set; }
        public bool BlackRightCastling { get; set; }
        public Position EnPassant { get; set; }
        public int HalfMoveClock { get; set; }
        public int FullMoveNumber { get; set; }
    }
}
