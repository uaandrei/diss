using Chess.Infrastructure.Enums;

namespace Chess.Infrastructure
{
    public class Ply
    {
        public Position From { get; set; }
        public Position To { get; set; }
        public PieceColor SideColor { get; set; }
        public object Capture { get; set; }

        public Ply(Position from, Position to, PieceColor side, object capture)
        {
            From = from;
            To = to;
            SideColor = side;
            Capture = capture;
        }
    }
}
