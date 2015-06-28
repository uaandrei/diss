using Chess.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.ViewModels
{
    public class LoadSavedGameViewModel : ViewModelBase, ILoadSavedGameViewModel
    {
        public string Fen { get { return SelectedSavedGame.Fen; } }
        public List<SavedGameInfo> SavedGames { get; set; }
        public SavedGameInfo SelectedSavedGame { get; set; }

        public LoadSavedGameViewModel()
        {
            GameModule.LoggedUser.ForceReloadSavedGames();
            SavedGames = GameModule.LoggedUser.SavedGames;
        }
    }
}
