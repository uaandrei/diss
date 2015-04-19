using Chess.Pieces;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class PawnMove : IMoveStrategy
    {
        private PieceColor _color;
        private IPieceContainer _container;

        public PawnMove(IPieceContainer container, PieceColor color)
        {
            _container = container;
            _color = color;
        }

        public IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();

            var x = position.X;
            var y = position.Y + GetDirection();

            var posibleMove = new Position(x, y);
            if (posibleMove.IsInBounds() && _container.IsFree(posibleMove))
                positions.Add(posibleMove);

            return positions;
        }

        public IList<Position> GetAttacks(Position position)
        {
            var positions = new List<Position>();

            var x = position.X;
            var y = position.Y + GetDirection();
            var posibleAttack = new Position(x - 1, y);
            if (!_container.IsFree(posibleAttack))
                positions.AddPieceIfPossible(position, posibleAttack, _container);
            posibleAttack = new Position(x + 1, y);
            if (!_container.IsFree(posibleAttack))
                positions.AddPieceIfPossible(position, posibleAttack, _container);
            return positions;
        }

        private int GetDirection()
        {
            return _color == PieceColor.Black ? 1 : -1;
        }
    }
}
