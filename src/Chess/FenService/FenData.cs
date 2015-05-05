using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using FenService.Interfaces;

namespace FenService
{
    public class FenData : IFenData
    {
        public IPieceInfo[] PieceInfos { get; set; }
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
