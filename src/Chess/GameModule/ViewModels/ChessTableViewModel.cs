using Chess.Business.Interfaces;
using Chess.Infrastructure;
using Chess.Infrastructure.Behaviours;
using Chess.Infrastructure.Enums;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chess.Game.ViewModels
{
    public class ChessTableViewModel : ViewModelBase, IChessTableViewModel
    {
        #region Memebers
        private IEventAggregator _eventAggregator;
        private IGameTable _gameTable;
        public ObservableCollection<IChessSquareViewModel> Squares { get; private set; }

        private bool _moveAllowed;
        public bool MoveAllowed
        {
            get { return _moveAllowed; }
            set
            {
                _moveAllowed = value;
                NotifyPropertyChanged();
            }
        }

        private string _infoText;
        public string InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Initialization

        public ChessTableViewModel(IEventAggregator eg, IGameTable gt)
        {
            MoveAllowed = true;
            _gameTable = gt;
            _eventAggregator = eg;
            _gameTable.Start();
            InitializeTableSquares();
            InitializeEventHandlers();
            RedrawTable();
        }

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.SquareSelectedEvent>().Subscribe(OnSquareSelected);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.PlayerChangedEvent>().Subscribe(OnPlayerChanged);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.RefreshTableEvent>().Subscribe(DoRefreshTable);
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

        #endregion

        #region Event Handlers

        private void OnSquareSelected(IChessSquareViewModel square)
        {
            _gameTable.ParseInput(square.Position);
            RedrawTable();
        }

        private void OnPlayerChanged(object player)
        {
            MoveAllowed = !_gameTable.CurrentPlayer.IsAutomatic;
            InfoText = string.Format("Player to move: {0}\nUI: {1}\nWaiting...", _gameTable.CurrentPlayer.Name, _gameTable.CurrentPlayer.IsAutomatic);
        }

        private void DoRefreshTable(object obj)
        {
            RedrawTable();
        }

        #endregion

        #region Methods

        private void RedrawTable()
        {
            Squares.ForEach(s =>
            {
                s.SquareState = SquareState.Empty;
                s.Representation = string.Empty;
            });
            _gameTable.GetPieces().ForEach(piece =>
                Squares.Single(s => s.Position == piece.CurrentPosition).Representation = string.Format("{0}\n{1}", piece.Color, piece.Type)
            );
            SelectSquare(_gameTable.SelectedSquare);
            _gameTable.TableAttacks.ForEach(a => SetSquareState(a, SquareState.PosibleAttack));
            _gameTable.TableMoves.ForEach(a => SetSquareState(a, SquareState.PosibleMove));
        }

        private void SelectSquare(Position pos)
        {
            if (pos == null)
                return;
            SetSquareState(pos, SquareState.Selected);
        }

        private void SetSquareState(Position pos, SquareState state)
        {
            Squares.Single(s => s.Position == pos).SquareState = state;
        }

        #endregion
    }
}