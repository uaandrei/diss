using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
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

        public IEnumerable<IPiece> Pieces { get { return _pieces; } }
        public IPlayer CurrentPlayer { get { return _playerEnumerator.Current; } }
        public IEnumerable<Position> TableMoves { get { return _moves; } }
        public IEnumerable<Position> TableAttacks { get { return _attacks; } }
        public Position SelectedSquare { get { return _selectedPiece == null ? null : _selectedPiece.CurrentPosition; } }

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
            if (_selectedPiece != null && _selectedPiece.CurrentPosition == userInput)
                return;
            
            _moves.Clear();
            _attacks.Clear();
            if (MoveWasHandled(userInput))
            {
                CyclePlayerTurn();
                return;
            }
            _selectedPiece = _pieces.FirstOrDefault(p => p.CurrentPosition == userInput);
            if (_selectedPiece != null)
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

            var whitePlayer = new HumanPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.White).ToList(), 1);
            var blackPlayer = new HumanPlayer(_pieces.Where(p => p.Color == Infrastructure.Enums.PieceColor.Black).ToList(), 2);
            _players = new[] { whitePlayer, blackPlayer };
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
        }

        private bool MoveWasHandled(Position position)
        {
            if (_selectedPiece == null)
                return false;

            if (!_allAvailableMoves.Contains(position) || !CurrentPlayer.OwnsPiece(_selectedPiece))
                return false;

            _selectedPiece.Move(position);
            _selectedPiece = null;
            return true;
        }

        private void SetMovesForSelectedPiece()
        {
            _allAvailableMoves.Clear();
            if (_selectedPiece == null)
                return;

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
