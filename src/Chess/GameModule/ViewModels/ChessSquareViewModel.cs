using Chess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.ViewModels
{
    public class ChessSquareViewModel : ViewModelBase
    {
        public int Index { get; private set; }

        public ChessSquareViewModel(int index)
        {
            Index = index;
        }
    }
}
