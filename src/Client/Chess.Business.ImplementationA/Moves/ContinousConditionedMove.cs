using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public abstract class ContinousConditionedMove : MoveStrategyBase
    {
        protected System.Collections.Generic.IList<Position> GeneratePositions(System.Func<IEnumerable<IPiece>, Position, bool> addCondition, IPiece currentPiece, IEnumerable<IPiece> allPieces, int xOffset = 0, int yOffset = 0)
        {
            var positions = new System.Collections.Generic.List<Position>();
            var posiblePosition = new Position(currentPiece.CurrentPosition.X + xOffset, currentPiece.CurrentPosition.Y + yOffset);

            while (posiblePosition.IsInBounds())
            {
                if (addCondition(allPieces, posiblePosition))
                    positions.Add(posiblePosition);
                if (!allPieces.IsFree(posiblePosition))
                    break;

                posiblePosition = new Position(posiblePosition.X + xOffset, posiblePosition.Y + yOffset);
            }
            return positions;
        }
    }
}
