﻿using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Business.ImplementationA
{
    public class GameTable : IGameTable
    {
        private IEnumerable<IPlayer> _players;
        private IEnumerator<IPlayer> _playerEnumerator;
        private IList<IPiece> _pieces;
        private IPiece _selectedPiece;
        private List<Position> _allAvailableMoves;
        private List<Position> _moves;
        private List<Position> _attacks;
        private IEventAggregator _eventAggregator;

        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        public IPlayer CurrentPlayer { get { return _playerEnumerator.Current; } }
        public IEnumerable<Position> TableMoves { get { return _moves; } }
        public IEnumerable<Position> TableAttacks { get { return _attacks; } }
        public Position SelectedSquare { get { return _selectedPiece == null ? null : _selectedPiece.CurrentPosition; } }

        public GameTable(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Start()
        {
            _allAvailableMoves = new List<Position>();
            _moves = new List<Position>();
            _attacks = new List<Position>();
            InitializePlayersAndPieces();
            CyclePlayerTurn();
        }

        public void ParseInput(Position userInput)
        {
            if (IsPieceSelected() && _selectedPiece.CurrentPosition == userInput)
                return;

            _moves.Clear();
            _attacks.Clear();
            if (MoveWasHandled(userInput))
            {
                CyclePlayerTurn();
                _selectedPiece = null;
                return;
            }
            _selectedPiece = _pieces.GetPiece(userInput);
            if (IsPieceSelected())
            {
                SetMovesForSelectedPiece();
            }
        }

        public IEnumerable<IPiece> GetPieces()
        {
            return _pieces;
        }

        private void InitializePlayersAndPieces()
        {

            var pieceFactory = new PieceFactory();
            _pieces = pieceFactory.GetPieces();
            _pieces.ForEach(p => p.PieceMoving += OnPieceMoving);

            var whitePlayer = new HumanPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.White), 1);
            //var blackPlayer = new HumanPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.Black).ToList(), 2);
            var blackPlayer = new DummyComputerPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.Black), 2);
            _players = new IPlayer[] { whitePlayer, blackPlayer };
            _playerEnumerator = _players.GetEnumerator();
        }

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            if (_pieces.IsOccupied(newPosition))
            {
                var attackedPiece = _pieces.Single(p => p.CurrentPosition == newPosition);
                _pieces.Remove(attackedPiece);
            }
        }

        private void CyclePlayerTurn()
        {
            if (!_playerEnumerator.MoveNext())
            {
                _playerEnumerator.Reset();
                _playerEnumerator.MoveNext();
            }
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.PlayerChangedEvent>().Publish(CurrentPlayer);
            CurrentPlayer.Act(this);
        }

        private bool MoveWasHandled(Position position)
        {
            if (!IsPieceSelected())
                return false;

            if (!_allAvailableMoves.Contains(position) || !CurrentPlayer.OwnsPiece(_selectedPiece))
                return false;

            _selectedPiece.Move(position);
            return true;
        }

        private bool IsPieceSelected()
        {
            return _selectedPiece != null;
        }

        private void SetMovesForSelectedPiece()
        {
            _allAvailableMoves.Clear();
            _moves.AddRange(_selectedPiece.GetAvailableMoves(Pieces));
            _attacks.AddRange(_selectedPiece.GetAvailableAttacks(Pieces));
            _allAvailableMoves.AddRange(TableMoves);
            _allAvailableMoves.AddRange(TableAttacks);
        }
    }

    public static class PieceExtensions
    {
        public static bool IsFree(this IEnumerable<IPiece> pieces, int x, int y)
        {
            return pieces.IsFree(new Position(x, y));
        }

        public static bool IsFree(this IEnumerable<IPiece> pieces, Position pos)
        {
            return !pieces.Any(p => p.CurrentPosition == pos);
        }

        public static bool IsOccupied(this IEnumerable<IPiece> pieces, int x, int y)
        {
            return pieces.IsOccupied(new Position(x, y));
        }

        public static bool IsOccupied(this IEnumerable<IPiece> pieces, Position pos)
        {
            return !pieces.IsFree(pos);
        }

        public static IPiece GetPiece(this IEnumerable<IPiece> pieces, Position pos)
        {
            return pieces.FirstOrDefault(p => p.CurrentPosition == pos);
        }
    }
}
