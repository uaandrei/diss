
namespace Chess
{
    public class Position
    {
        //TODO: piece property
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(Position p)
            : this(p.X, p.Y)
        {

        }

        public Position()
        {
            X = 0;
            Y = 0;
        }
    }
}
