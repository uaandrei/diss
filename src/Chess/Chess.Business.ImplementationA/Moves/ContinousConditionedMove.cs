using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;

namespace Chess.Business.ImplementationA.Moves
{
    public abstract class ContinousConditionedMove : MoveStrategyBase
    {
        public ContinousConditionedMove(IPieceContainer container)
            : base(container)
        {
        }

        protected System.Collections.Generic.IList<Position> GeneratePositions(System.Func<IPieceContainer, Position, bool> addCondition, Position position, int xOffset = 0, int yOffset = 0)
        {
            var positions = new System.Collections.Generic.List<Position>();
            var posiblePosition = new Position(position.X + xOffset, position.Y + yOffset);

            while (posiblePosition.IsInBounds())
            {
                if (addCondition(_container, posiblePosition))
                    positions.Add(posiblePosition);
                if (!_container.IsFree(posiblePosition))
                    break;

                posiblePosition = new Position(posiblePosition.X + xOffset, posiblePosition.Y + yOffset);
            }
            return positions;
        }
    }
}
