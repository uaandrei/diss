using System;
using System.Collections.Generic;

namespace Chess.Moves
{
    public class DiagonalMove : ContinousConditionedMove
    {
        public DiagonalMove(int[,] matrix, Position position)
            : base(matrix, position)
        {
        }

        public override IList<Position> GetMoves()
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v == 0, 1, 1));
            positions.AddRange(GeneratePositions(v => v == 0, -1, -1));
            positions.AddRange(GeneratePositions(v => v == 0, 1, -1));
            positions.AddRange(GeneratePositions(v => v == 0, -1, 1));
            return positions;
        }

        public override IList<Position> GetAttacks()
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v != 0, 1, 1));
            positions.AddRange(GeneratePositions(v => v != 0, -1, -1));
            positions.AddRange(GeneratePositions(v => v != 0, 1, -1));
            positions.AddRange(GeneratePositions(v => v != 0, -1, 1));
            return positions;
        }
    }
}
