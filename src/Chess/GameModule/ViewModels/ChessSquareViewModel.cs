using Chess.Infrastructure.Behaviours;
using Chess.Infrastructure.Enums;
using Chess.Infrastructure.Events;
using Chess.Pieces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Windows.Input;

namespace Chess.Game.ViewModels
{
    public class ChessSquareViewModel : ViewModelBase, IChessSquareViewModel
    {
        private IEventAggregator _eventAggregator;
        private IPiece _piece;
        public IPiece Piece { get { return _piece; } }
        public string Representation { get { return _piece == null ? string.Empty : _piece.Type.ToString(); } }
        public int Index { get; private set; }

        private SquareStates _squareState;
        public SquareStates SquareState
        {
            get { return _squareState; }
            set
            {
                _squareState = value;
                NotifyPropertyChanged();
            }
        }


        //TODO: Rename this command =>> SquareSelectedCommand || OnSelectCommand
        public ICommand SquareChangeCommand { get; set; }

        public ChessSquareViewModel(int index, IPiece piece = null)
        {
            _piece = piece;
            Index = index;
            SquareChangeCommand = new DelegateCommand(ExecuteSquareChange);
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        private void ExecuteSquareChange()
        {
            _eventAggregator.GetEvent<SquareSelectedEvent>().Publish(this);
        }

        public override string ToString()
        {
            return string.Format("index:{0} {1}", Index, _piece);
        }
    }
}
