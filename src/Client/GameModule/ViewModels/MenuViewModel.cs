using System.Linq;
using Chess.Business.Interfaces;
using Chess.Game.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using System.IO;
using System.Windows.Input;
using Chess.Infrastructure;
using Microsoft.Practices.Prism.PubSubEvents;
using Chess.Infrastructure.Events;
using System;
namespace Chess.Game.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private IGameTable _gameTable;
        private IEventAggregator _eventAggregator;

        #region Commands
        public ICommand NewGameCommand { get; private set; }
        public ICommand LoadGameCommand { get; private set; }
        public ICommand SaveGameCommand { get; private set; }
        public ICommand UndoLastMoveCommand { get; private set; }
        public ICommand OptionsCommand { get; private set; }
        public ICommand MoveCommand { get; private set; }
        public ICommand UserCommand { get; set; }
        #endregion

        public MenuViewModel(IGameTable gt, IEventAggregator ea)
        {
            _eventAggregator = ea;
            _gameTable = gt;
            LoadGameCommand = new DelegateCommand(LoadGameFromFen);
            SaveGameCommand = new DelegateCommand(SaveGame);
            OptionsCommand = new DelegateCommand(OptionsExecute);
            UndoLastMoveCommand = new DelegateCommand(UndoLastMove);
            NewGameCommand = new DelegateCommand(NewGame);
            MoveCommand = new DelegateCommand(MoveExecute);
            UserCommand = new DelegateCommand(UserExecute);
            UserExecute();
        }

        private void UserExecute()
        {
            var loginView = ServiceLocator.Current.GetInstance<IView<ILoginViewModel>>();
            var res = loginView.ShowView();
            if (res.HasValue && res.Value)
            {
                _eventAggregator.GetEvent<MessageEvent>().Publish(
                    new MessageInfo(
                        4000,
                        string.Format("Welcome '{0}'!", GameModule.LoggedUser != null ? GameModule.LoggedUser.Name : "Guest")
                    )
                );
            }
        }

        private void MoveExecute()
        {
            _gameTable.CurrentPlayer.Act(_gameTable);
        }

        private void OptionsExecute()
        {
            var optionsView = ServiceLocator.Current.GetInstance<IView<IOptionsViewModel>>();
            optionsView.ViewModel.IsWhiteAI = _gameTable.Players.First(p => p.Color == Infrastructure.Enums.PieceColor.White).IsAutomatic;
            optionsView.ViewModel.IsBlackAI = _gameTable.Players.First(p => p.Color == Infrastructure.Enums.PieceColor.Black).IsAutomatic;
            optionsView.ViewModel.Difficulty = _gameTable.Difficulty;
            var res = optionsView.ShowView();
            if (res.HasValue && res.Value)
            {
                var vm = optionsView.ViewModel;
                _gameTable.ChangePlayers(vm.IsBlackAI, vm.IsWhiteAI);
                _gameTable.Difficulty = optionsView.ViewModel.Difficulty;
            }
        }

        private void NewGame()
        {
            _gameTable.StartNewGame();
        }

        private void UndoLastMove()
        {
            _gameTable.UndoLastMove();
        }

        private void LoadGameFromFen()
        {
            if (GameModule.LoggedUser != null)
            {
                var loadSavedGameView = ServiceLocator.Current.GetInstance<IView<ILoadSavedGameViewModel>>();
                var res = loadSavedGameView.ShowView();
                if (res.HasValue && res.Value)
                {
                    _gameTable.LoadFromFen(loadSavedGameView.ViewModel.Fen);
                    _eventAggregator.GetEvent<MessageEvent>().Publish(
                        new MessageInfo(
                            4000,
                            string.Format("Game: '{0}' Loaded!", loadSavedGameView.ViewModel.Fen)
                        )
                    );
                }
                return;
            }
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
            if (GameModule.LoggedUser != null)
            {
                var nameView = new NameGameToSaveView();
                var res = nameView.ShowDialog();
                if (res.HasValue && res.Value)
                {
                    var game = new Persistance.SavedGameInfo
                    {
                        Comment = nameView.GameName.Text,
                        Id = Guid.NewGuid().ToString(),
                        LastSaved = DateTime.Now,
                        Fen = _gameTable.GetFen()
                    };
                    GameModule.LoggedUser.SaveGame(game);
                    _eventAggregator.GetEvent<MessageEvent>().Publish(
                        new MessageInfo(
                            4000,
                            string.Format("Game: '{0}' Saved!", game.Fen)
                        )
                    );
                }
                return;
            }
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
    }
}
