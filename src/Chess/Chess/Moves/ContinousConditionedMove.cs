namespace Chess.Moves
{
    public abstract class ContinousConditionedMove : IMoveStrategy
    {
        protected int[,] _matrix;

        public ContinousConditionedMove(int[,] matrix)
        {
            _matrix = matrix;
        }

        public abstract System.Collections.Generic.IList<Position> GetMoves(Position position);
        public abstract System.Collections.Generic.IList<Position> GetAttacks(Position position);

        protected System.Collections.Generic.IList<Position> GeneratePositions(System.Func<int, bool> addCondition, Position position, int xOffset = 0, int yOffset = 0)
        {
            var positions = new System.Collections.Generic.List<Position>();
            var posiblePosition = new Position(position.X + xOffset, position.Y + yOffset);

            while (posiblePosition.IsInBounds())
            {
                if (addCondition(_matrix[posiblePosition.X, posiblePosition.Y]))
                    positions.Add(posiblePosition);
                if (_matrix[posiblePosition.X, posiblePosition.Y] != 0)
                    break;

                posiblePosition = new Position(posiblePosition.X + xOffset, posiblePosition.Y + yOffset);
            }
            return positions;
        }
    }
}
