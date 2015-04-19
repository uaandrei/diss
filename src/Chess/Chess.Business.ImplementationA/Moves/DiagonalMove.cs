﻿using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class DiagonalMove : ContinousConditionedMove
    {
        public DiagonalMove(IPieceContainer container)
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

        protected override List<Position> GenerateAttacks(Position position)
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
