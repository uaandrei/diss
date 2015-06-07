using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
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

        public bool IsAutomatic { get { return true; } }
        public IEnumerable<Interfaces.Piece.IPiece> Pieces { get { return _pieces; } }
        public string RequestURI { get { return "http://localhost:9000/chess"; } }
        public int MoveOrder { get { return _moveOrder; } }
        public string Name { get { return string.Format("Smart AI {0}", _moveOrder); } }

        public SmartComputerPlayer(IEnumerable<IPiece> list, int moveOrder)
        {
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
            var client = new HttpClient();
            var response = client.GetAsync(string.Format("{0}?fen={1}&depth={2}", RequestURI, WebUtility.UrlEncode(gameTable.GetFen()), 5)).Result;
            var resultString = response.Content.ReadAsStringAsync().Result;
            var moveString = resultString.Split(';')[0];
            var fromFile = moveString[1];
            int fromRank = moveString[2] - '0';
            var toFile = moveString[3];
            int toRank = moveString[4] - '0';
            var fromPosition = new Position(fromRank, fromFile);
            var toPosition = new Position(toRank, toFile);
            gameTable.ParseInput(fromPosition);
            gameTable.ParseInput(toPosition);
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(this);
        }
    }
}
