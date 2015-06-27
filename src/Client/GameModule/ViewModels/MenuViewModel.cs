using System.Linq;
using Chess.Business.Interfaces;
using Chess.Game.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using System.IO;
using System.Windows.Input;
namespace Chess.Game.ViewModels
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        private IGameTable _gameTable;

        #region Commands
        public ICommand NewGameCommand { get; private set; }
        public ICommand LoadGameCommand { get; private set; }
        public ICommand SaveGameCommand { get; private set; }
        public ICommand UndoLastMoveCommand { get; private set; }
        public ICommand OptionsCommand { get; private set; }
        public ICommand MoveCommand { get; private set; }
        #endregion

        public MenuViewModel(IGameTable gt)
        {
            _gameTable = gt;
            LoadGameCommand = new DelegateCommand(LoadGameFromFen);
            SaveGameCommand = new DelegateCommand(SaveGame);
            OptionsCommand = new DelegateCommand(OptionsExecute);
            UndoLastMoveCommand = new DelegateCommand(UndoLastMove);
            NewGameCommand = new DelegateCommand(NewGame);
            MoveCommand = new DelegateCommand(MoveExecute);
        }

        private void MoveExecute()
        {
            _gameTable.CurrentPlayer.Act(_gameTable);
        }

        private void OptionsExecute()
        {
            var window = ServiceLocator.Current.GetInstance<OptionsView>();
            window.ViewModel.IsWhiteAI = _gameTable.Players.First(p => p.Color == Infrastructure.Enums.PieceColor.White).IsAutomatic;
            window.ViewModel.IsBlackAI = _gameTable.Players.First(p => p.Color == Infrastructure.Enums.PieceColor.Black).IsAutomatic;
            var res = window.ShowDialog();
            if (res.HasValue && res.Value)
            {
                var vm = window.ViewModel;
                _gameTable.ChangePlayers(vm.IsBlackAI, vm.IsWhiteAI);
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
    }
}
