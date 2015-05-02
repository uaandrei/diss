using Chess.Business.ImplementationA.Pieces;
using Chess.Business.ImplementationA.Players;
using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
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

        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        public IPlayer CurrentPlayer { get { return _playerSwitchSystem.CurrentPlayer; } }
        public IEnumerable<Position> TableMoves { get { return _moves; } }
        public IEnumerable<Position> TableAttacks { get { return _attacks; } }
        public Position SelectedSquare { get { return _selectedPiece == null ? null : _selectedPiece.CurrentPosition; } }
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
            throw new System.NotImplementedException();
        }

        public void LoadFromFen(string fen)
        {
            throw new System.NotImplementedException();
        }

        #region Private
        private void InitializePlayersAndPieces()
        {
            var pieceFactory = new PieceFactory();
            _pieces = pieceFactory.GetPieces();
            _pieces.ForEach(p => p.PieceMoving += OnPieceMoving);
            var whitePlayer = new DummyComputerPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.White), 1);
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
            if (_pieces.IsOccupied(newPosition))
            {
                var attackedPiece = _pieces.Single(p => p.CurrentPosition == newPosition);
                _pieces.Remove(attackedPiece);
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
