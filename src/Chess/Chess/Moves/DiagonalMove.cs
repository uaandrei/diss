using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class DiagonalMove : ContinousConditionedMove
    {
        public DiagonalMove(int[,] matrix)
            : base(matrix)
        {
        }

        public override IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v == 0, position, 1, 1));
            positions.AddRange(GeneratePositions(v => v == 0, position, -1, -1));
            positions.AddRange(GeneratePositions(v => v == 0, position, 1, -1));
            positions.AddRange(GeneratePositions(v => v == 0, position, -1, 1));
            return positions;
        }

        public override IList<Position> GetAttacks(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v != 0, position, 1, 1));
            positions.AddRange(GeneratePositions(v => v != 0, position, -1, -1));
            positions.AddRange(GeneratePositions(v => v != 0, position, 1, -1));
            positions.AddRange(GeneratePositions(v => v != 0, position, -1, 1));
            return positions;
        }
    }
}
