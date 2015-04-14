using Chess.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chess.Game.ViewModels
{
    public class ChessSquareViewModel : ViewModelBase
    {
        public int Index { get; private set; }
        public ICommand SquareChangeCommand { get; set; }

        public ChessSquareViewModel(int index)
        {
            Index = index;
            SquareChangeCommand = new DelegateCommand(ExecuteSquareChange);

        }

        private void ExecuteSquareChange()
        {
        }
    }
}
