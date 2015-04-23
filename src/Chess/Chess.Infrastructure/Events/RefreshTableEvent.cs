using Microsoft.Practices.Prism.PubSubEvents;

namespace Chess.Infrastructure.Events
{
    public class RefreshTableEvent : PubSubEvent<object>
    {
    }
}
