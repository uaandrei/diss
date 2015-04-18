using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class DiagonalMove : ContinousConditionedMove
    {
        public DiagonalMove(Chess.Pieces.IPieceContainer container)
            : base(container)
        {
        }

        public override IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, 1, 1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, -1, -1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, 1, -1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), position, -1, 1));
            return positions;
        }

        public override IList<Position> GetAttacks(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, 1, 1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, -1, -1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, 1, -1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), position, -1, 1));
            return positions;
        }
    }
}
