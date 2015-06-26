using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chess.Game.ViewModels
{
    public class MoveHistoryViewModel : ViewModelBase, IMoveHistoryViewModel
    {
        private IEventAggregator _eventAggregator;
        private Move _selectedMove;

        public BindingList<Move> MovesMade { get; set; }
        public ICommand OnMoveSelectedCommand { get; set; }

        public MoveHistoryViewModel(IEventAggregator eg)
        {
            _eventAggregator = eg;
            MovesMade = new BindingList<Move>();
            OnMoveSelectedCommand = new DelegateCommand<Move>(OnMoveSelected);
            InitializeEventHandlers();
        }

        private void OnMoveSelected(Move move)
        {
            if (move == null || move == _selectedMove)
                return;
            _eventAggregator.GetEvent<DrawMoveEvent>().Publish(move);
            _selectedMove = move;
        }

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.MovedPieceEvent>().Subscribe(OnPieceMoved);
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.MoveUndoEvent>().Subscribe(OnMoveUndo);
        }

        private void OnMoveUndo(object obj)
        {
            if (MovesMade.Count > 0)
                MovesMade.RemoveAt(MovesMade.Count - 1);
        }

        private void OnPieceMoved(Infrastructure.Events.Move obj)
        {
            MovesMade.Add(obj);
        }
    }
}
