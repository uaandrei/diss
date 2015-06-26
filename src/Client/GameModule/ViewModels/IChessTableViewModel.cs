using Chess.Game.ViewModels;
using System.Collections.ObjectModel;

namespace Chess.Game.ViewModels
{
    public interface IChessTableViewModel
    {
        ObservableCollection<IChessSquareViewModel> Squares { get; }
    }
}
