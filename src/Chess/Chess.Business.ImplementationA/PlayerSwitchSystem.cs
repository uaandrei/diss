using Chess.Business.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA
{
    internal class PlayerSwitchSystem
    {
        private IEnumerator<IPlayer> _playerEnumerator;
        private IEventAggregator _eventAggregator;
        public IPlayer CurrentPlayer { get { return _playerEnumerator.Current; } }

        public PlayerSwitchSystem(IEnumerator<IPlayer> enumerator)
        {
            _playerEnumerator = enumerator;
            _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        internal void NextTurn(IGameTable gameTable)
        {
            if (!_playerEnumerator.MoveNext())
            {
                _playerEnumerator.Reset();
                _playerEnumerator.MoveNext();
            }
            _eventAggregator.GetEvent<Chess.Infrastructure.Events.PlayerChangedEvent>().Publish(CurrentPlayer);
            CurrentPlayer.Act(gameTable);
        }
    }
}
