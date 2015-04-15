using Chess.Infrastructure;
using Chess.Pieces;
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
        private IPiece _piece;
        public int Index { get; private set; }
        public string Representation { get { return _piece == null ? string.Empty : _piece.Type.ToString(); } }

        //TODO: Rename this command =>> SquareSelectedCommand || OnSelectCommand
        public ICommand SquareChangeCommand { get; set; }

        public ChessSquareViewModel(int index, IPiece piece = null)
        {
            _piece = piece;
            Index = index;
            SquareChangeCommand = new DelegateCommand(ExecuteSquareChange);

        }

        private void ExecuteSquareChange()
        {
        }

        public override string ToString()
        {
            return string.Format("index:{0} {1}", Index, _piece);
        }
    }
}
