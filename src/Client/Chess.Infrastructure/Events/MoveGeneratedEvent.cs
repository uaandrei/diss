using Microsoft.Practices.Prism.PubSubEvents;
using SmartChessService.DataContracts;

namespace Chess.Infrastructure.Events
{
    public class MoveGeneratedEvent : PubSubEvent<ChessEngineResult>
    {
    }
}
