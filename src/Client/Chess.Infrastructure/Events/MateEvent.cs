using Microsoft.Practices.Prism.PubSubEvents;
namespace Chess.Infrastructure.Events
{
    public class MateEvent:PubSubEvent<bool>
    {
    }
}
