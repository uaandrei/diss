using Microsoft.Practices.Prism.PubSubEvents;

namespace Chess.Infrastructure.Events
{
    public class SquareSelectedEvent : PubSubEvent<object>
    {
    }
}
