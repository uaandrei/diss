﻿namespace Chess
{
    public class Position
    {
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

        public override bool Equals(object obj)
        {
            var position = obj as Position;
            if(position==null)
                return base.Equals(obj);

            return position.X == X
                && position.Y == Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
