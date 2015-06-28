using Chess.Business.Interfaces;
using Chess.Game.Views;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chess.Game.ViewModels
{
    public class ChessTableViewModel : ViewModelBase, IChessTableViewModel
    {
        #region Members
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
        #endregion

        #region Initialization
        public ChessTableViewModel(IEventAggregator eg, IGameTable gt)
        {
            MoveAllowed = true;
            _gameTable = gt;
            _eventAggregator = eg;
            _gameTable.StartNewGame();
            InitializeTableSquares();
            InitializeEventHandlers();
            RedrawTable();
        }

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<SquareSelectedEvent>().Subscribe(OnSquareSelected);
            _eventAggregator.GetEvent<RefreshTableEvent>().Subscribe(DoRefreshTable);
            _eventAggregator.GetEvent<DrawMoveEvent>().Subscribe(OnDrawMove);
            _eventAggregator.GetEvent<PromotePieceEvent>().Subscribe(OnPiecePromotion);
        }

        private void OnPiecePromotion(Position obj)
        {
            var promotionView = ServiceLocator.Current.GetInstance<IView<IPromotionViewModel>>();
            var result = promotionView.ShowView();
            if (result.HasValue && result.Value)
                _gameTable.GetPieces().First(p => p.CurrentPosition == obj).Type = promotionView.ViewModel.PieceType;
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
        private void OnDrawMove(Move obj)
        {
            Squares.ForEach(s =>
            {
                if (s.SquareState == SquareState.History)
                    s.SquareState = SquareState.Empty;
            });
            Squares.First(s => s.Position == obj.From).SquareState = SquareState.History;
            Squares.First(s => s.Position == obj.To).SquareState = SquareState.History;
        }

        private void OnSquareSelected(object obj)
        {
            var square = obj as IChessSquareViewModel;
            if (square == null)
                return;

            if (_gameTable.CurrentPlayer.IsAutomatic)
                return;
            _gameTable.ParseInput(square.Position);
            RedrawTable();
        }

        private void DoRefreshTable(object obj)
        {
            RedrawTable();
        }
        #endregion

        #region Methods
        private void RedrawTable()
        {
            MoveAllowed = !_gameTable.CurrentPlayer.IsAutomatic;
            Squares.ForEach(s =>
            {
                s.SquareState = SquareState.Empty;
                s.Representation = string.Empty;
            });
            _gameTable.GetPieces().ForEach(piece =>
                Squares.Single(s => s.Position == piece.CurrentPosition).Representation = string.Format("{0}{1}", piece.Color, piece.Type)
            );
            SelectSquare(_gameTable.SelectedSquare);
            if (_gameTable.CurrentPlayer.IsAutomatic)
                return;
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
            if (pos == null)
                return;
            Squares.Single(s => s.Position == pos).SquareState = state;
        }
        #endregion
    }
}