using Microsoft.Practices.Prism.PubSubEvents;

namespace Chess.Infrastructure.Events
{
    public class PlayerChangedEvent : PubSubEvent<object>
    {
    }
}
