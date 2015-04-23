using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chess.Business.ImplementationA
{
    public class DummyComputerPlayer : IPlayer
    {
        private List<IPiece> _pieces;
        private int _moveOrder;
        private IEventAggregator _eventAggregator;
        public bool IsAutomatic { get { return true; } }
        public string RequestURI { get { return "Dummy API"; } }
        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        public int MoveOrder { get { return _moveOrder; } }
        public string Name { get { return string.Format("Dummy AI {0}", _moveOrder); } }

        public DummyComputerPlayer(List<IPiece> list, int moveOrder)
        {
            _pieces = list;
            _moveOrder = moveOrder;
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public void Move(Position from, Position to)
        {

        }

        public bool OwnsPiece(IPiece piece)
        {
            return true;
        }

        public void Act(IGameTable gameTable)
        {
            var moveTask = new Task(GenerateMove, gameTable);
            moveTask.Start();
        }

        private void GenerateMove(object state)
        {
            var gameTable = state as IGameTable;
            IPiece pieceToMove;
            Thread.Sleep(500);
            var availablePositions = new List<Position>();
            do
            {
                availablePositions.Clear();
                var random = new Random();
                pieceToMove = _pieces[random.Next(0, _pieces.Count)];
                availablePositions.AddRange(pieceToMove.GetAvailableAttacks(gameTable.GetPieces()));
                availablePositions.AddRange(pieceToMove.GetAvailableMoves(gameTable.GetPieces()));
            } while (availablePositions.Count <= 0);
            var random1 = new Random();
            var move = availablePositions[random1.Next(0, availablePositions.Count)];
            gameTable.ParseInput(pieceToMove.CurrentPosition);
            gameTable.ParseInput(move);
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(this);
        }
    }
}
