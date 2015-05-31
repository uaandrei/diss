using Chess.Game.ViewModels;
using System.Collections.ObjectModel;

namespace Chess.Infrastructure.Behaviours
{
    public interface IChessTableViewModel
    {
        ObservableCollection<IChessSquareViewModel> Squares { get; }
    }
}
