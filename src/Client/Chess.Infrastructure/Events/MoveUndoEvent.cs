using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Infrastructure.Events
{
    public class MoveUndoEvent:PubSubEvent<object>
    {
    }
}
