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
        private const int MateScore = -29000;

        public static ChessEngineResult GenerateMove(string fen, int depth)
        {
            var result = GetResponse(fen, depth);
            return result;
        }

        public static bool IsMate(string fen)
        {
            var result = GetResponse(fen, 5);
            return result.Score == MateScore;
        }

        private static ChessEngineResult GetResponse(string fen, int depth)
        {

            var client = new HttpClient();
            var request = string.Format(requestFormat, requestUri, WebUtility.UrlEncode(fen), depth);
            var result = client.GetAsync(request).Result.Content.ReadAsAsync<ChessEngineResult>().Result;
            return result;
        }
    }
}
