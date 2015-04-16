using Chess.Pieces;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class PawnMove : IMoveStrategy
    {
        private int[,] _matrix;
        private PieceColor _color;

        public PawnMove(int[,] matrix, PieceColor color)
        {
            _matrix = matrix;
            _color = color;
        }

        public IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();

            var x = position.X;
            var y = position.Y + GetDirection();

            var posibleMove = new Position(x, y);
            if (posibleMove.IsInBounds() && _matrix[posibleMove.X, posibleMove.Y] == 0)
                positions.Add(posibleMove);

            return positions;
        }

        public IList<Position> GetAttacks(Position position)
        {
            var positions = new List<Position>();

            var x = position.X;
            var y = position.Y + GetDirection();
            var posibleAttack = new Position(x - 1, y);
            if (posibleAttack.IsInBounds() && _matrix[posibleAttack.X, posibleAttack.Y] != 0)
                positions.Add(posibleAttack);
            posibleAttack = new Position(x + 1, y);
            if (posibleAttack.IsInBounds() && _matrix[posibleAttack.X, posibleAttack.Y] != 0)
                positions.Add(posibleAttack);
            return positions;
        }

        private int GetDirection()
        {
            return _color == PieceColor.Black ? 1 : -1;
        }
    }
}
