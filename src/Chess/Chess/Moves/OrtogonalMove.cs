using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Moves
{
    public class OrtogonalMove : ContinousConditionedMove
    {
        public OrtogonalMove(int[,] matrix, Position position)
            : base(matrix, position)
        {
        }

        public override IList<Position> GetMoves()
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v == 0, xOffset: -1));
            positions.AddRange(GeneratePositions(v => v == 0, xOffset: 1));
            positions.AddRange(GeneratePositions(v => v == 0, yOffset: -1));
            positions.AddRange(GeneratePositions(v => v == 0, yOffset: 1));
            return positions;
        }

        public override IList<Position> GetAttacks()
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions(v => v != 0, xOffset: -1));
            positions.AddRange(GeneratePositions(v => v != 0, xOffset: 1));
            positions.AddRange(GeneratePositions(v => v != 0, yOffset: -1));
            positions.AddRange(GeneratePositions(v => v != 0, yOffset: 1));
            return positions;
        }
    }
}
