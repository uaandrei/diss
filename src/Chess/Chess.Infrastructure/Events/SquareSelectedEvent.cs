using Chess.Infrastructure.Behaviours;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Chess.Infrastructure.Events
{
    public class SquareSelectedEvent : PubSubEvent<IChessSquareViewModel>
    {
    }
}
