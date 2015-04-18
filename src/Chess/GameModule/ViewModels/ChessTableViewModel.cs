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
        private Infrastructure.Events.SquareSelectedEvent _onSquareSelectedEvent;
        private Pieces.IPiece _selectedPiece;
        private IEventAggregator EventAggregator { get; set; }
        private TableState _state;
        public ObservableCollection<IChessSquareViewModel> Squares { get; private set; }

        public ChessTableViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            _gameTable = new GameTable();
            _state = TableState.PieceNotSelected;
            SetupTable();
            InitializeEvents();
        }

        private void SetupTable()
        {
            var whitePieces = _gameTable.Pieces.Take(16).ToList();
            var blackPieces = _gameTable.Pieces.Except(whitePieces).ToList();
            var squares = new List<IChessSquareViewModel>();

            foreach (var piece in whitePieces)
            {
                squares.Add(new ChessSquareViewModel(8 * piece.CurrentPosition.Y + piece.CurrentPosition.X, piece));
            }
            for (int i = 16; i <= 47; i++)
            {
                squares.Add(new ChessSquareViewModel(i));
            }
            foreach (var piece in blackPieces)
            {
                squares.Add(new ChessSquareViewModel(8 * piece.CurrentPosition.Y + piece.CurrentPosition.X, piece));
            }
            Squares = new ObservableCollection<IChessSquareViewModel>(squares.OrderBy(p => p.Index).ToList());
        }

        private void InitializeEvents()
        {
            _onSquareSelectedEvent = EventAggregator.GetEvent<Chess.Infrastructure.Events.SquareSelectedEvent>();
            _onSquareSelectedEvent.Subscribe(OnSquareSelected);
        }

        private void OnSquareSelected(IChessSquareViewModel selectedSquare)
        {
            if (_state == TableState.PieceSelected)
            {
                var selectedSquarePosisition = new Position(selectedSquare.Index % 8, selectedSquare.Index / 8);
                _selectedPiece.Move(selectedSquarePosisition);
                selectedSquare.Piece = _selectedPiece;
                ClearSquares();
                _state = TableState.PieceNotSelected;
                return;
            }
            ClearSquares();
            selectedSquare.SquareState = SquareStates.Selected;
            _selectedPiece = selectedSquare.Piece;
            if (selectedSquare.Piece == null)
            {
                _state = TableState.PieceNotSelected;
                return;
            }

            _state = TableState.PieceSelected;
            MarkPossibleAttacksForSelectedPiece();
            MarkPossibleMovesForSelectedPiece();
        }

        private void ClearSquares()
        {

            foreach (var square in Squares)
            {
                square.SquareState = SquareStates.Empty;
            }
        }

        private void MarkPossibleMovesForSelectedPiece()
        {
            var moves = _selectedPiece.GetAvailableMoves();
            foreach (var move in moves)
            {
                var squareIndex = move.Y * 8 + move.X;
                Squares[squareIndex].SquareState = SquareStates.PosibleMove;
            }
        }

        private void MarkPossibleAttacksForSelectedPiece()
        {
            var attacks = _selectedPiece.GetAvailableAttacks();
            foreach (var attack in attacks)
            {
                var squareIndex = attack.Y * 8 + attack.X;
                Squares[squareIndex].SquareState = SquareStates.PosibleAttack;
            }
        }

    }
}