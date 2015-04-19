using Chess.Infrastructure;
using System;

namespace Chess.Business.ImplementationA.Exceptions
{
    public class PieceNotFoundException : Exception
    {
        private Position _position;
        public PieceNotFoundException(Position p)
        {
            _position = p;
        }

        public override string Message
        {
            get
            {
                return string.Format("Piece not found in position {0}", _position);
            }
        }
    }
}
