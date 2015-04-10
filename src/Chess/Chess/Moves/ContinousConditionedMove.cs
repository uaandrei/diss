namespace Chess.Moves
{
    public abstract class ContinousConditionedMove : IMoveStrategy
    {
        protected int[,] _matrix;
        protected Position _position;

        public ContinousConditionedMove(int[,] matrix, Position position)
        {
            _matrix = matrix;
            _position = position;
        }

        public abstract System.Collections.Generic.IList<Position> GetMoves();
        public abstract System.Collections.Generic.IList<Position> GetAttacks();

        protected System.Collections.Generic.IList<Position> GeneratePositions(System.Func<int, bool> addCondition, int xOffset = 0, int yOffset = 0)
        {
            var positions = new System.Collections.Generic.List<Position>();
            var posiblePosition = new Position(_position.X + xOffset, _position.Y + yOffset);

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
