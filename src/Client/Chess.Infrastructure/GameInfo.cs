using Chess.Infrastructure.Enums;

namespace Chess.Infrastructure
{
    public class GameInfo
    {
        public bool Wkca { get; set; }
        public bool Wqca { get; set; }
        public bool Bkca { get; set; }
        public bool Bqca { get; set; }
        public PieceColor ColorToMove { get; set; }
        public Position EnPassant { get; set; }
        public int HalfMoves { get; set; }
        public int FullMoves { get; set; }
    }
}
