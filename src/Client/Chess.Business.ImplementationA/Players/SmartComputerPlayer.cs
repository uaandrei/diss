using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Chess.Business.ImplementationA.Players
{
    public class SmartComputerPlayer : IPlayer
    {
        private IEnumerable<IPiece> _pieces;
        private int _moveOrder;
        private IEventAggregator _eventAggregator;
        private const string requestFormat = "{0}?fen={1}&depth={2}";
        private const string requestUri = "http://localhost:9000/chess";

        public bool IsAutomatic { get { return true; } }
        public IEnumerable<Interfaces.Piece.IPiece> Pieces { get { return _pieces; } }
        public string RequestURI { get { return requestUri; } }
        public int MoveOrder { get { return _moveOrder; } }
        public string Name { get { return string.Format("Smart AI {0}", _moveOrder); } }
        public PieceColor Color { get; private set; }

        public SmartComputerPlayer(IEnumerable<IPiece> list, int moveOrder)
        {
            if (list == null || list.Count() <= 0)
                throw new ArgumentException("list");

            Color = list.First().Color;
            _pieces = list;
            _moveOrder = moveOrder;
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public void Move(Infrastructure.Position from, Infrastructure.Position to)
        {
        }

        public bool OwnsPiece(Interfaces.Piece.IPiece piece)
        {
            return _pieces.Contains(piece);
        }

        public void Act(IGameTable gameTable)
        {
            var resultString = GetMoveResponse(gameTable.GetFen(), 5);
            var moveString = resultString.Split(';')[0];
            var fromFile = moveString[0];
            int fromRank = moveString[1] - '0';
            var toFile = moveString[2];
            int toRank = moveString[3] - '0';
            var fromPosition = new Position(fromRank, fromFile);
            var toPosition = new Position(toRank, toFile);
            gameTable.ParseInput(fromPosition);
            gameTable.ParseInput(toPosition);
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(this);
        }

        // TODO: remove this static shit
        private static string GetMoveResponse(string fen, int depth)
        {
            var client = new HttpClient();
            var response = client.GetAsync(string.Format(requestFormat, requestUri, WebUtility.UrlEncode(fen), 5)).Result;
            var resultString = response.Content.ReadAsStringAsync().Result;
            return resultString.Replace("\"", "");
        }

        public static bool IsMate(string fen)
        {
            var resultString = GetMoveResponse(fen, 5);
            var moveScoreString = resultString.Split(';')[1];
            var score = Convert.ToInt32(moveScoreString.Replace("/", "").Replace("\\", ""));
            return score == -29000;
        }
    }
}
