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
        public bool IsPerm;

        public MessageInfo(int ms, string msg, bool isPerm = false)
        {
            MsgTimeMs = ms;
            Message = msg;
            IsPerm = isPerm;
        }
    }
}
