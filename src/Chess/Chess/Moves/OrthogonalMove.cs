using System.Collections.Generic;

namespace Chess.Moves
{
    public class OrthogonalMove : ContinousConditionedMove
    {
        public OrthogonalMove(Chess.Pieces.IPieceContainer container)
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
