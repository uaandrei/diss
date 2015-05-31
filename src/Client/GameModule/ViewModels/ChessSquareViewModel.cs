using Chess.Infrastructure;
using Chess.Infrastructure.Behaviours;
using Chess.Infrastructure.Enums;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;

namespace Chess.Game.ViewModels
{
    public class ChessSquareViewModel : ViewModelBase, IChessSquareViewModel
    {
        private IEventAggregator _eventAggregator;
        public Position Position { get; private set; }
        public int Index { get { return Position.Y * 8 + Position.X; } }

        private SquareState _squareState;
        public SquareState SquareState
        {
            get { return _squareState; }
            set
            {
                _squareState = value;
                NotifyPropertyChanged();
            }
        }

        private string _representation;
        public string Representation
        {
            get { return _representation; }
            set
            {
                _representation = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SquareSelectedCommand { get; set; }

        public ChessSquareViewModel(Position position)
        {
            Position = position;
            SquareSelectedCommand = new DelegateCommand(OnSquareSelected);
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        public override string ToString()
        {
            return string.Format("{0}", Position);
        }

        private void OnSquareSelected()
        {
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.SquareSelectedEvent>().Publish(this);
        }
    }
}
