using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Moves
{
    public class PawnMove : IMoveStrategy
    {
        private int[,] _matrix;
        private Position _position;
        private PieceColor _color;

        public PawnMove(int[,] matrix, Position position, PieceColor color)
        {
            _matrix = matrix;
            _position = position;
            _color = color;
        }

        public IList<Position> GetMoves()
        {
            var positions = new List<Position>();

            var yDirection = GetYDirection();
            var x = _position.X;
            var y = _position.Y + yDirection;
            if (_matrix[x, y] == 0)
                positions.Add(x, y);

            return positions;
        }

        public IList<Position> GetAttacks()
        {
            var positions = new List<Position>();

            var yDirection = GetYDirection();
            var x = _position.X;
            var y = _position.Y + yDirection;
            if (_matrix[x - 1, y] != 0)
                positions.Add(x - 1, y);
            if (_matrix[x + 1, y] != 0)
                positions.Add(x + 1, y);
            return positions;
        }

        private int GetYDirection()
        {
            return _color == PieceColor.Black ? -1 : 1;
        }
    }
}
