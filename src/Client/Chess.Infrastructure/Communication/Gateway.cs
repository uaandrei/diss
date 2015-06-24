using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartChessService.DataContracts;
using System.Net;
using System.Net.Http;

namespace Chess.Infrastructure.Communication
{
    public class Gateway
    {
        private const string requestFormat = "{0}?fen={1}&depth={2}";
        private const string requestUri = "http://localhost:9000/chess";
        private static IEventAggregator _eventAggregator;

        static Gateway()
        {
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public static void RequestMove(string fen, int depth)
        {
            var client = new HttpClient();
            var request = string.Format(requestFormat, requestUri, WebUtility.UrlEncode(fen), depth);
            client.GetAsync(request).ContinueWith(x => { 
                var result = x.Result.Content.ReadAsAsync<ChessEngineResult>().Result;
                _eventAggregator.GetEvent<MoveGeneratedEvent>().Publish(result);
            });
        }

        public static void RequestIsChess(string fen)
        {

        }
    }
}
