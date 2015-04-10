using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Moves
{
    public class OrthogonalMove : ContinousConditionedMove
    {
        public OrthogonalMove(int[,] matrix)
            : base(matrix)
        {
        }

        public override IList<Position> GetMoves(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v == 0, position, xOffset: -1));
            positions.AddRange(GeneratePositions(v => v == 0, position, xOffset: 1));
            positions.AddRange(GeneratePositions(v => v == 0, position, yOffset: -1));
            positions.AddRange(GeneratePositions(v => v == 0, position, yOffset: 1));
            return positions;
        }

        public override IList<Position> GetAttacks(Position position)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v != 0, position, xOffset: -1));
            positions.AddRange(GeneratePositions(v => v != 0, position, xOffset: 1));
            positions.AddRange(GeneratePositions(v => v != 0, position, yOffset: -1));
            positions.AddRange(GeneratePositions(v => v != 0, position, yOffset: 1));
            return positions;
        }
    }
}
