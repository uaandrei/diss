using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Exceptions
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
