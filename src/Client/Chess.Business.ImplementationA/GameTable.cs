using Chess.Business.ImplementationA.Pieces;
using Chess.Business.ImplementationA.Players;
using Chess.Business.ImplementationA.Rules;
using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Chess.Infrastructure.Events;
using Chess.Infrastructure.Logging;
using Chess.Infrastructure.Names;
using FenService.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Business.ImplementationA
{
    public class GameTable : IGameTable
    {
        #region Members
        private IEnumerable<IPlayer> _players;
        private IList<IPiece> _pieces;
        private IPiece _selectedPiece;
        private List<Position> _allAvailableMoves = new List<Position>();
        private List<Position> _moves = new List<Position>();
        private List<Position> _attacks = new List<Position>();
        private PlayerSwitchSystem _playerSwitchSystem;
        private IFenService _fenService;
        private IEventAggregator _eventAggregator;
        private IPieceFactory _pieceFactory;
        private Stack<string> _fenStack;
        private Dictionary<string, IRule> _rules;
        private GameInfo _gameInfo;

        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        public IEnumerable<IPlayer> Players { get { return _players; } }
        public IPlayer CurrentPlayer { get { return _playerSwitchSystem.CurrentPlayer; } }
        public IEnumerable<Position> TableMoves { get { return _moves; } }
        public IEnumerable<Position> TableAttacks { get { return _attacks; } }
        public Position SelectedSquare { get { return _selectedPiece == null ? null : _selectedPiece.CurrentPosition; } }
        public int Difficulty { get; set; }
        public string Id { get; private set; }
        #endregion

        #region ctor
        public GameTable(IFenService fenService, IEventAggregator evtAggr, IPieceFactory pieceFactory)
        {
            Difficulty = 5;
            _pieceFactory = pieceFactory;
            _fenService = fenService;
            _eventAggregator = evtAggr;
            _fenStack = new Stack<string>();
            _rules = new RuleProvider(this).Rules;
            _gameInfo = new GameInfo();
        }
        #endregion

        #region Methods
        public void StartNewGame()
        {
            _gameInfo = new GameInfo()
            {
                Bkca = true,
                Wkca = true,
                Bqca = true,
                Wqca = true,
                ColorToMove = PieceColor.White,
                EnPassant = null,
                FullMoves = 0,
                HalfMoves = 0
            };
            Id = Guid.NewGuid().ToString();
            InitializePlayersAndPieces();
            _playerSwitchSystem = new PlayerSwitchSystem(_players.GetEnumerator());
            ResetBoardState(true);
        }

        public void LoadFromFen(string fen, bool clearStack = true)
        {
            // TODO: set id
            var fenData = _fenService.GetData(fen);
            _pieces.Clear();
            foreach (var piece in fenData.PieceInfos.Select(p => p.ToPiece()))
            {
                piece.PieceMoving += OnPieceMoving;
                _pieces.Add(piece);
            }
            _gameInfo.CopyFrom(fenData.GameInfo);
            ResetBoardState(clearStack);
        }

        public void ParseInput(Position userInput)
        {
            if (MoveCanBeHandled(userInput))
            {
                PlayerMove(userInput);
                ClearAllMoves();
            }
            else
            {
                ClearAllMoves();
                _selectedPiece = _pieces.GetPiece(userInput);
                SetMovesForSelectedPiece();
            }
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.RefreshTableEvent>().Publish(_gameInfo);
        }

        public void UndoLastMove()
        {
            if (_fenStack.Count == 0)
                return;

            var lastFen = _fenStack.Pop();
            LoadFromFen(lastFen, false);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.MoveUndoEvent>().Publish(null);
        }

        public void SetSelectedPiece(Position piecePosition)
        {
            _selectedPiece = _pieces.GetPiece(piecePosition);
            SetMovesForSelectedPiece();
        }

        public IEnumerable<IPiece> GetPieces()
        {
            return _pieces;
        }

        public string GetFen()
        {
            var fenData = new FenData
            {
                PieceInfos = _pieces.Select(p => p.GetInfo()).ToArray()
            };

            fenData.GameInfo.CopyFrom(_gameInfo);
            return _fenService.GetFen(fenData);
        }

        public void ChangePlayers(bool isBlackAI, bool isWhiteAI)
        {
            var currentPlayerColor = CurrentPlayer.Color;
            var blackPlayer = _players.First(p => p.Color == PieceColor.Black);
            var whitePlayer = _players.First(p => p.Color == PieceColor.White);
            if (isBlackAI && !blackPlayer.IsAutomatic)
                blackPlayer = new SmartComputerPlayer(_pieces.Where(p => p.Color == PieceColor.Black), blackPlayer.MoveOrder);
            if (!isBlackAI && blackPlayer.IsAutomatic)
                blackPlayer = new HumanPlayer(_pieces.Where(p => p.Color == PieceColor.Black), blackPlayer.MoveOrder);

            if (isWhiteAI && !whitePlayer.IsAutomatic)
                whitePlayer = new SmartComputerPlayer(_pieces.Where(p => p.Color == PieceColor.White), whitePlayer.MoveOrder);
            if (!isWhiteAI && whitePlayer.IsAutomatic)
                whitePlayer = new HumanPlayer(_pieces.Where(p => p.Color == PieceColor.White), whitePlayer.MoveOrder);

            _players = new IPlayer[] { whitePlayer, blackPlayer };
            _playerSwitchSystem = new PlayerSwitchSystem(_players.GetEnumerator());
            _playerSwitchSystem.SwitchToPlayer(currentPlayerColor);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.RefreshTableEvent>().Publish(this);
        }
        #region Private
        private void ResetBoardState(bool clearStack = true)
        {
            ClearAllMoves();
            _selectedPiece = null;
            _playerSwitchSystem.SwitchToPlayer(_gameInfo.ColorToMove);
            if (clearStack)
                ClearFenStack();

            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(_gameInfo);
        }

        private void InitializePlayersAndPieces()
        {
            _pieces = _pieceFactory.GetAllPieces();
            _pieces.ForEach(p => p.PieceMoving += OnPieceMoving);
            var whitePlayer = new HumanPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.White), 1);
            var blackPlayer = new SmartComputerPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.Black), 2);
            _players = new IPlayer[] { whitePlayer, blackPlayer };
        }

        private void PlayerMove(Position userInput)
        {
            _fenStack.Push(GetFen());
            var fromPos = new Position(_selectedPiece.CurrentPosition);
            _selectedPiece.Move(userInput);

            if (_rules[RuleNames.Chess].IsSatisfied())
            {
                UndoLastMove();
                _eventAggregator.GetEvent<Chess.Infrastructure.Events.MessageEvent>().Publish(new MessageInfo(1500, "Move illegal! King in check."));
            }
            else
            {
                _eventAggregator.GetEvent<Chess.Infrastructure.Events.MovedPieceEvent>().Publish(new Move(fromPos, userInput));
                _gameInfo.HalfMoves++;
                _gameInfo.FullMoves++;
                _selectedPiece = null;
                _playerSwitchSystem.NextTurn(this);
            }
            _gameInfo.ColorToMove = _playerSwitchSystem.CurrentPlayer.Color;
            if (_rules[RuleNames.Mate].IsSatisfied())
                _eventAggregator.GetEvent<Chess.Infrastructure.Events.MessageEvent>().Publish(new MessageInfo(0, "You lost! King in check-mate."));
        }

        private void ClearAllMoves()
        {
            _attacks.Clear();
            _moves.Clear();
            _allAvailableMoves.Clear();
        }

        private bool MoveCanBeHandled(Position userInput)
        {
            return IsPieceSelected()
                && !_selectedPiece.CurrentPosition.Equals(userInput)
                && _allAvailableMoves.Contains(userInput)
                && CurrentPlayer.OwnsPiece(_selectedPiece);
        }

        private void ClearFenStack()
        {
            while (_fenStack.Count > 0)
            {
                _fenStack.Pop();
                _eventAggregator.GetEvent<Chess.Infrastructure.Events.MoveUndoEvent>().Publish(null);
            }
        }

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            object capturedPiece = null;
            var logMessage = string.Format("piece:{0} => {1}", this, newPosition);
            if (_pieces.IsOccupied(newPosition))
            {
                var attackedPiece = _pieces.Single(p => p.CurrentPosition == newPosition);
                capturedPiece = attackedPiece;
                _pieces.Remove(attackedPiece);
                _gameInfo.HalfMoves = 0;
                logMessage = string.Format("{0} attacked:{1}", logMessage, attackedPiece);
            }
            HandleCastlingRights(piece, newPosition);
            if (piece.Type == PieceType.Pawn && !CurrentPlayer.IsAutomatic)
                HandlePromotion(piece, newPosition);

            Logger.Log(LogLevel.Info, logMessage);
        }

        private void HandleCastlingRights(IPiece piece, Position newPosition)
        {
            if (piece.Type == PieceType.King && !piece.HasMoved)
            {
                if (piece.Color == PieceColor.White)
                {
                    _gameInfo.Wkca = false;
                    _gameInfo.Wqca = false;
                    if (newPosition.ToAlgebraic() == "c1")
                    {
                        var qRook = _pieces.First(p => p.CurrentPosition.ToAlgebraic() == "a1");
                        qRook.Move(new Position("d1"));
                    }
                    if (newPosition.ToAlgebraic() == "g1")
                    {
                        var kRook = _pieces.First(p => p.CurrentPosition.ToAlgebraic() == "h1");
                        kRook.Move(new Position("f1"));
                    }
                }
                if (piece.Color == PieceColor.Black)
                {
                    _gameInfo.Bkca = false;
                    _gameInfo.Bqca = false;
                    if (newPosition.ToAlgebraic() == "c8")
                    {
                        var qRook = _pieces.First(p => p.CurrentPosition.ToAlgebraic() == "a8");
                        qRook.Move(new Position("d8"));
                    }
                    if (newPosition.ToAlgebraic() == "g8")
                    {
                        var kRook = _pieces.First(p => p.CurrentPosition.ToAlgebraic() == "h8");
                        kRook.Move(new Position("f8"));
                    }
                }
            }
            if (piece.Type == PieceType.Rook && piece.CurrentPosition.ToAlgebraic() == "a1")
            {
                _gameInfo.Wqca = false;
            }
            if (piece.Type == PieceType.Rook && piece.CurrentPosition.ToAlgebraic() == "h1")
            {
                _gameInfo.Wkca = false;
            }
            if (piece.Type == PieceType.Rook && piece.CurrentPosition.ToAlgebraic() == "a8")
            {
                _gameInfo.Bqca = false;
            }
            if (piece.Type == PieceType.Rook && piece.CurrentPosition.ToAlgebraic() == "h8")
            {
                _gameInfo.Bkca = false;
            }
        }

        private void HandlePromotion(IPiece piece, Position newPosition)
        {
            if ((piece.Color == PieceColor.White && newPosition.Rank == 8) ||
                (piece.Color == PieceColor.Black && newPosition.Rank == 1))
            {
                _eventAggregator.GetEvent<PromotePieceEvent>().Publish(piece.CurrentPosition);
            }
        }

        private void SetMovesForSelectedPiece()
        {
            if (!IsPieceSelected())
                return;
            _attacks.AddRange(_selectedPiece.GetAvailableAttacks(Pieces));
            _moves.AddRange(_selectedPiece.GetAvailableMoves(Pieces));
            _allAvailableMoves.AddRange(_attacks);
            _allAvailableMoves.AddRange(_moves);
        }

        private bool IsPieceSelected()
        {
            return _selectedPiece != null;
        }
        #endregion
        #endregion
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
