using Chess.Business.ImplementationA.Pieces;
using Chess.Business.ImplementationA.Players;
using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Events;
using Chess.Infrastructure.Logging;
using FenService.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
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
        private List<Position> _allAvailableMoves;
        private List<Position> _moves;
        private List<Position> _attacks;
        private PlayerSwitchSystem _playerSwitchSystem;
        private IFenService _fenService;
        private IEventAggregator _eventAggregator;
        private IPieceFactory _pieceFactory;

        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        public IPlayer CurrentPlayer { get { return _playerSwitchSystem.CurrentPlayer; } }
        public IEnumerable<Position> TableMoves { get { return _moves; } }
        public IEnumerable<Position> TableAttacks { get { return _attacks; } }
        public Position SelectedSquare { get { return _selectedPiece == null ? null : _selectedPiece.CurrentPosition; } }
        public Position MovedTo { get; private set; }
        [Dependency]
        public IRuleSystem RuleSystem { get; set; }
        #endregion

        #region ctor
        public GameTable(IFenService fenService, IEventAggregator evtAggr, IPieceFactory pieceFactory)
        {
            _pieceFactory = pieceFactory;
            _fenService = fenService;
            _eventAggregator = evtAggr;
        }
        #endregion

        #region Methods
        public void Start()
        {
            _allAvailableMoves = new List<Position>();
            _moves = new List<Position>();
            _attacks = new List<Position>();
            InitializePlayersAndPieces();
            _playerSwitchSystem = new PlayerSwitchSystem(_players.GetEnumerator());
            _playerSwitchSystem.NextTurn(this);
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
        }

        public IEnumerable<IPiece> GetPieces()
        {
            return _pieces;
        }

        public string GetFen()
        {
            // todo: maybe converters?
            var pieceInfos = (from p in _pieces
                              select new PieceInfo
                              {
                                  Color = p.Color,
                                  File = p.File,
                                  Rank = p.Rank,
                                  Type = p.Type
                              }).ToArray();
            var fenData = new FenData
            {
                PieceInfos = pieceInfos,
                // todo: improve, add color to player so i know which player has to move
                ColorToMove = _playerSwitchSystem.CurrentPlayer.Pieces.First().Color
            };
            return _fenService.GetFen(fenData);
        }

        public void LoadFromFen(string fen)
        {
            var fenData = _fenService.GetData(fen);
            // todo: maybe converters?
            _pieces = (from p in fenData.PieceInfos
                       select new ChessPiece(p.Rank, p.File, p.Color, p.Type)).ToArray<IPiece>();
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(this);
        }

        #region Private
        private void InitializePlayersAndPieces()
        {
            _pieces = _pieceFactory.GetAllPieces();
            _pieces.ForEach(p => p.PieceMoving += OnPieceMoving);
            var whitePlayer = new HumanPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.White), 1);
            var blackPlayer = new DummyComputerPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.Black), 2);
            _players = new IPlayer[] { whitePlayer, blackPlayer };
        }

        private void PlayerMove(Position userInput)
        {
            _selectedPiece.Move(userInput);
            _selectedPiece = null;
            _playerSwitchSystem.NextTurn(this);
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

        private void OnPieceMoving(IPiece piece, Position newPosition)
        {
            var logMessage = string.Format("piece:{0} => {1}", this, newPosition);
            if (_pieces.IsOccupied(newPosition))
            {
                var attackedPiece = _pieces.Single(p => p.CurrentPosition == newPosition);
                _pieces.Remove(attackedPiece);
                logMessage = string.Format("{0} attacked:{1}", logMessage, attackedPiece);
            }
            MovedTo = newPosition;
            Logger.Log(LogLevel.Info, logMessage);
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
