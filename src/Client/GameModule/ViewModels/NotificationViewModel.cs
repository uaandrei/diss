using Chess.Business.Interfaces;
using Chess.Infrastructure.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
namespace Chess.Game.ViewModels
{
    public class NotificationViewModel : ViewModelBase, INotificationViewModel
    {
        private IEventAggregator _eventAggregator;
        private object _lock = new object();
        private IGameTable _gameTable;

        private string _infoText;
        public string InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                NotifyPropertyChanged();
            }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }

        public NotificationViewModel(IEventAggregator evt, IGameTable gameTable)
        {
            _eventAggregator = evt;
            _gameTable = gameTable;
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            _eventAggregator.GetEvent<MessageEvent>().Subscribe(OnMessageEvent);
            _eventAggregator.GetEvent<RefreshTableEvent>().Subscribe(OnRefresh);
        }

        private void OnRefresh(object obj)
        {
            InfoText = string.Format("Color to move: {0} - {1}\nUI: {1}\nWaiting...", _gameTable.CurrentPlayer.Color, _gameTable.CurrentPlayer.Name, _gameTable.CurrentPlayer.IsAutomatic);
        }

        private void OnMessageEvent(MessageInfo obj)
        {
            DisplayMessageHandle(obj);
        }

        private void DisplayMessageHandle(MessageInfo messageInfo)
        {
            new Task(() =>
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    lock (_lock)
                    {
                        Message = messageInfo.Message;
                        if (messageInfo.MsgTimeMs > 0)
                        {
                            Thread.Sleep(1500);
                            Message = string.Empty;
                        }
                    }
                })
            ).Start();
        }
    }
}
