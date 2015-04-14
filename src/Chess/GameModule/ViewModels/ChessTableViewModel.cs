using System.Collections.ObjectModel;

namespace Chess.Game.ViewModels
{
    public class ChessTableViewModel
    {
        public ObservableCollection<ChessSquareViewModel> Squares { get; set; }

        public ChessTableViewModel()
        {
            Squares = new ObservableCollection<ChessSquareViewModel>();
            Squares.Add(new ChessSquareViewModel(0));
            Squares.Add(new ChessSquareViewModel(1));
            Squares.Add(new ChessSquareViewModel(2));
            Squares.Add(new ChessSquareViewModel(3));
            Squares.Add(new ChessSquareViewModel(4));
            Squares.Add(new ChessSquareViewModel(5));
            Squares.Add(new ChessSquareViewModel(6));
            Squares.Add(new ChessSquareViewModel(7));
            Squares.Add(new ChessSquareViewModel(8));
            Squares.Add(new ChessSquareViewModel(9));
            Squares.Add(new ChessSquareViewModel(10));
            Squares.Add(new ChessSquareViewModel(11));
            Squares.Add(new ChessSquareViewModel(12));
            Squares.Add(new ChessSquareViewModel(13));
            Squares.Add(new ChessSquareViewModel(14));
            Squares.Add(new ChessSquareViewModel(15));
            Squares.Add(new ChessSquareViewModel(16));
            Squares.Add(new ChessSquareViewModel(17));
            Squares.Add(new ChessSquareViewModel(18));
            Squares.Add(new ChessSquareViewModel(19));
            Squares.Add(new ChessSquareViewModel(20));
            Squares.Add(new ChessSquareViewModel(21));
            Squares.Add(new ChessSquareViewModel(22));
            Squares.Add(new ChessSquareViewModel(23));
            Squares.Add(new ChessSquareViewModel(24));
            Squares.Add(new ChessSquareViewModel(25));
            Squares.Add(new ChessSquareViewModel(26));
            Squares.Add(new ChessSquareViewModel(27));
            Squares.Add(new ChessSquareViewModel(28));
            Squares.Add(new ChessSquareViewModel(29));
            Squares.Add(new ChessSquareViewModel(30));
            Squares.Add(new ChessSquareViewModel(31));
            Squares.Add(new ChessSquareViewModel(32));
            Squares.Add(new ChessSquareViewModel(33));
            Squares.Add(new ChessSquareViewModel(34));
            Squares.Add(new ChessSquareViewModel(35));
            Squares.Add(new ChessSquareViewModel(36));
            Squares.Add(new ChessSquareViewModel(37));
            Squares.Add(new ChessSquareViewModel(38));
            Squares.Add(new ChessSquareViewModel(39));
            Squares.Add(new ChessSquareViewModel(40));
            Squares.Add(new ChessSquareViewModel(41));
            Squares.Add(new ChessSquareViewModel(42));
            Squares.Add(new ChessSquareViewModel(43));
            Squares.Add(new ChessSquareViewModel(44));
            Squares.Add(new ChessSquareViewModel(45));
            Squares.Add(new ChessSquareViewModel(46));
            Squares.Add(new ChessSquareViewModel(47));
            Squares.Add(new ChessSquareViewModel(48));
            Squares.Add(new ChessSquareViewModel(49));
            Squares.Add(new ChessSquareViewModel(50));
            Squares.Add(new ChessSquareViewModel(51));
            Squares.Add(new ChessSquareViewModel(52));
            Squares.Add(new ChessSquareViewModel(53));
            Squares.Add(new ChessSquareViewModel(54));
            Squares.Add(new ChessSquareViewModel(55));
            Squares.Add(new ChessSquareViewModel(56));
            Squares.Add(new ChessSquareViewModel(57));
            Squares.Add(new ChessSquareViewModel(58));
            Squares.Add(new ChessSquareViewModel(59));
            Squares.Add(new ChessSquareViewModel(60));
            Squares.Add(new ChessSquareViewModel(61));
            Squares.Add(new ChessSquareViewModel(62));
            Squares.Add(new ChessSquareViewModel(63));
        }
    }
}