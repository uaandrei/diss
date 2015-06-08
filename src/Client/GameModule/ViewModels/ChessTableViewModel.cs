using Chess.Business.Interfaces;
using Chess.Infrastructure;
using Chess.Infrastructure.Behaviours;
using Chess.Infrastructure.Enums;
using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
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
        private object _lock = new object();
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

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand LoadGameCommand { get; private set; }
        public ICommand SaveGameCommand { get; private set; }
        public ICommand UndoLastMoveCommand { get; private set; }
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
            LoadGameCommand = new DelegateCommand(LoadGameFromFen);
            SaveGameCommand = new DelegateCommand(SaveGame);
            UndoLastMoveCommand = new DelegateCommand(UndoLastMove);
        }

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.SquareSelectedEvent>().Subscribe(OnSquareSelected);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.PlayerChangedEvent>().Subscribe(OnPlayerChanged);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.RefreshTableEvent>().Subscribe(DoRefreshTable);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.MessageEvent>().Subscribe(DisplayMessageHandle);
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
        private void OnSquareSelected(object obj)
        {
            var square = obj as IChessSquareViewModel;
            if (square == null)
                return;

            _gameTable.ParseInput(square.Position);
            RedrawTable();
        }

        private void OnPlayerChanged(object player)
        {
            MoveAllowed = !_gameTable.CurrentPlayer.IsAutomatic;
            RedrawTable();
        }

        private void DoRefreshTable(object obj)
        {
            RedrawTable();
        }

        private void DisplayMessageHandle(MessageInfo messageInfo)
        {
            new Task(() =>
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    lock (_lock)
                    {
                        Message = messageInfo.Message;
                        if (messageInfo.MsgTimeMs > 0)
                        {
                            Thread.Sleep(1500);
                            Message = string.Empty;
                        }
                    }
                })
            ).Start();
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
                Squares.Single(s => s.Position == piece.CurrentPosition).Representation = string.Format("{0}{1}", piece.Color, piece.Type)
            );
            SelectSquare(_gameTable.SelectedSquare);
            _gameTable.TableAttacks.ForEach(a => SetSquareState(a, SquareState.PosibleAttack));
            _gameTable.TableMoves.ForEach(a => SetSquareState(a, SquareState.PosibleMove));
            SetSquareState(_gameTable.MovedTo, SquareState.LastMove);
            InfoText = string.Format("Color to move: {0} - {1}\nUI: {1}\nWaiting...", _gameTable.CurrentPlayer.Color, _gameTable.CurrentPlayer.Name, _gameTable.CurrentPlayer.IsAutomatic);
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

        private void UndoLastMove()
        {
            _gameTable.UndoLastMove();
        }

        private void LoadGameFromFen()
        {
            var openFileDialog = GetDialog<Microsoft.Win32.OpenFileDialog>();
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                var path = openFileDialog.FileName;
                using (var fileReader = new StreamReader(path))
                {
                    _gameTable.LoadFromFen(fileReader.ReadToEnd());
                }
            }
        }

        private void SaveGame()
        {
            var saveFileDialog = GetDialog<Microsoft.Win32.SaveFileDialog>();
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                var path = saveFileDialog.FileName;
                using (var fileWriter = new StreamWriter(path))
                {
                    fileWriter.WriteLine(_gameTable.GetFen());
                }
            }
        }

        public Microsoft.Win32.FileDialog GetDialog<D>() where D : Microsoft.Win32.FileDialog, new()
        {
            var fileDialog = new D();
            fileDialog.DefaultExt = ".txt";
            fileDialog.Filter = "*Text files (*.txt)|*.txt";
            return fileDialog;

        }
        #endregion
    }
}