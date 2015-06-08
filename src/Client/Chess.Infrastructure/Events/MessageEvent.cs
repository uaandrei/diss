using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Infrastructure.Events
{
    public class MessageEvent : PubSubEvent<MessageInfo>
    {
    }

    public struct MessageInfo
    {
        public int MsgTimeMs;
        public string Message;

        public MessageInfo(int ms, string msg)
        {
            MsgTimeMs = ms;
            Message = msg;
        }
    }
}
