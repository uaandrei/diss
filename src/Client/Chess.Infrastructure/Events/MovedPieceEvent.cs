using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Infrastructure.Events
{
    public class MovedPieceEvent : PubSubEvent<Move>
    {
    }

    public class Move
    {
        public Position From { get; private set; }
        public Position To { get; private set; }
        public Move(Position from, Position to)
        {
            From = from;
            To = to;
        }
    }
}
