using Chess.Infrastructure.Enums;

namespace FenService.Interfaces
{
    public class PieceInfo : IPieceInfo
    {
        public PieceColor Color { get; set; }
        public PieceType Type { get; set; }
        public int Rank { get; set; }
        public char File { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1} {2}{3}",Rank, File, Type, Color);
        }
    }
}
