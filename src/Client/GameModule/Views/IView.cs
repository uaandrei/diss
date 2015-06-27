using System.ComponentModel;

namespace Chess.Game.Views
{
    public interface IView<T>
    {
        T ViewModel { get; }
        bool? ShowView();
    }
}
