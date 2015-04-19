using Chess.Business.Interfaces.Move;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class WhitePawnMove : MoveStrategyBase
    {
        public WhitePawnMove(IPieceContainer container)
            : base(container)
        {
        }

        public override IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();

            var posibleMove = new Position(position.X, position.Y - 1);
            if (posibleMove.IsInBounds() && _container.IsFree(posibleMove))
                positions.Add(posibleMove);

            return positions;
        }

        protected override List<Position> GenerateAttacks(Position position)
        {
            var positions = new List<Position>();
            var x = position.X;
            var y = position.Y - 1;
            var posibleAttack = new Position(x - 1, y);
            if (!_container.IsFree(posibleAttack))
                positions.Add(posibleAttack);
            posibleAttack = new Position(x + 1, y);
            if (!_container.IsFree(posibleAttack))
                positions.Add(posibleAttack);
            return positions;
        }
    }
}
