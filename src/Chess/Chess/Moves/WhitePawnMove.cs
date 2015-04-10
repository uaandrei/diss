using System.Collections.Generic;

namespace Chess.Moves
{
    public class WhitePawnMove : IMoveStrategy
    {
        private int[,] _matrix;

        public WhitePawnMove(int[,] matrix)
        {
            _matrix = matrix;
        }

        public IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();

            var x = position.X;
            var y = position.Y + 1;
            if (_matrix[x, y] == 0)
                positions.Add(x, y);

            return positions;
        }

        public IList<Position> GetAttacks(Position position)
        {
            var positions = new List<Position>();

            var x = position.X;
            var y = position.Y + 1;
            if (_matrix[x - 1, y] != 0)
                positions.Add(x - 1, y);
            if (_matrix[x + 1, y] != 0)
                positions.Add(x + 1, y);
            return positions;
        }
    }
}
