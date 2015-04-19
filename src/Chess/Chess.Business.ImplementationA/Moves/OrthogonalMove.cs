using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class OrthogonalMove : ContinousConditionedMove
    {
        public OrthogonalMove(IPieceContainer container)
            : base(container)
        {
        }

        public override IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, xOffset: -1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, xOffset: 1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, yOffset: -1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, yOffset: 1));
            return positions;
        }

        public override IList<Position> GetAttacks(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, xOffset: -1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, xOffset: 1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, yOffset: -1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, yOffset: 1));
            return positions;
        }
    }
}
