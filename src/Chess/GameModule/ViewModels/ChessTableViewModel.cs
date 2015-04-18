using Chess.Infrastructure.Behaviours;
using Chess.Infrastructure.Enums;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chess.Game.ViewModels
{
    public class ChessTableViewModel : ViewModelBase, IChessTableViewModel
    {
        private GameTable _gameTable;
        private IEventAggregator _eventAggregator;
        private IList<Pieces.IPiece> _pieces;
        private Pieces.IPiece _selectedSquarePiece;
        private List<Position> _availableMoves;
        public ObservableCollection<IChessSquareViewModel> Squares { get; private set; }

        public ChessTableViewModel(IEventAggregator eventAggregator)
        {
            _gameTable = new GameTable();
            _availableMoves = new List<Position>();
            _pieces = _gameTable.Pieces;
            _eventAggregator = eventAggregator;
            InitializeTableSquares();
            InitializeEventHandlers();
            RedrawTable();
        }

        #region Event Handlers

        private void OnSquareSelected(IChessSquareViewModel square)
        {
            if (_selectedSquarePiece != null && _selectedSquarePiece.CurrentPosition == square.Position)
                return;
            RedrawTable();
            if (MoveWasHandled(square))
                return;
            SelectSquare(square);
            _selectedSquarePiece = _pieces.FirstOrDefault(p => p.CurrentPosition == square.Position);
            if (_selectedSquarePiece != null)
            {
                ColorAndSetMovesAndAttacksForPiece(_selectedSquarePiece);
            }
        }

        #endregion

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.SquareSelectedEvent>().Subscribe(OnSquareSelected);
        }

        private bool MoveWasHandled(IChessSquareViewModel square)
        {
            if (_selectedSquarePiece == null)
                return false;

            if (!_availableMoves.Contains(square.Position))
                return false;

            _selectedSquarePiece.Move(square.Position);
            _selectedSquarePiece = null;
            RedrawTable();
            return true;
        }

        private void InitializeTableSquares()
        {
            Squares = new ObservableCollection<IChessSquareViewModel>();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Squares.Add(new ChessSquareViewModel(new Position(x, y)));
                }
            }
        }

        private void SelectSquare(IChessSquareViewModel square)
        {
            square.SquareState = SquareState.Selected;
        }

        private void ColorAndSetMovesAndAttacksForPiece(Pieces.IPiece squarePiece)
        {
            _availableMoves.Clear();
            if (_selectedSquarePiece == null)
                return;

            var availableAttacks = squarePiece.GetAvailableAttacks();
            _availableMoves.AddRange(availableAttacks);
            foreach (var attack in availableAttacks)
            {
                Squares.First(p => p.Position == attack).SquareState = SquareState.PosibleAttack;
            }
            var availableMoves = squarePiece.GetAvailableMoves();
            _availableMoves.AddRange(availableMoves);
            foreach (var move in availableMoves)
            {
                Squares.First(p => p.Position == move).SquareState = SquareState.PosibleMove;
            }
        }

        private void RedrawTable()
        {
            foreach (var square in Squares)
            {
                square.SquareState = SquareState.Empty;
                square.Representation = string.Empty;
            }
            foreach (var piece in _pieces)
            {
                Squares.First(s => s.Position == piece.CurrentPosition).Representation = string.Format("{0}\n{1}", piece.Color, piece.Type);
            }
        }
    }
}