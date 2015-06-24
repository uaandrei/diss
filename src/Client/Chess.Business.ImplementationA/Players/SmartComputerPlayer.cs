using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Communication;
using Chess.Infrastructure.Enums;
using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Chess.Business.ImplementationA.Players
{
    public class SmartComputerPlayer : IPlayer
    {
        private IEnumerable<IPiece> _pieces;
        private int _moveOrder;
        private IEventAggregator _eventAggregator;
        private IGameTable _gameTable;

        public bool IsAutomatic { get { return true; } }
        public IEnumerable<Interfaces.Piece.IPiece> Pieces { get { return _pieces; } }
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
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<MoveGeneratedEvent>().Subscribe(OnMoveGenerated);
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
            _gameTable = gameTable;
            Gateway.RequestMove(gameTable.GetFen(), gameTable.Difficulty);
        }

        private void OnMoveGenerated(SmartChessService.DataContracts.ChessEngineResult result)
        {
            var fromPosition = new Position(result.FromRank, result.FromFile);
            var toPosition = new Position(result.ToRank, result.ToFile);
            _gameTable.ParseInput(fromPosition);
            _gameTable.ParseInput(toPosition);
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(this);
        }

        public static bool IsMate(string fen)
        {
            return false;
            //var resultString = GetMoveResponse(fen, 5);
            //var moveScoreString = resultString.Split(';')[1];
            //var score = Convert.ToInt32(moveScoreString.Replace("/", "").Replace("\\", ""));
            //return score == -29000;
        }
    }
}
