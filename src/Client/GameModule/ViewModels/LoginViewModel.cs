using Chess.Game.Views;
using Chess.Infrastructure.Events;
using Chess.Persistance;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Windows.Input;

namespace Chess.Game.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private IEventAggregator _eventAggregator;

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
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

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand EnterAsGuestCommand { get; set; }

        public LoginViewModel(IEventAggregator eg)
        {
            _eventAggregator = eg;
            LoginCommand = new DelegateCommand<LoginView>(OnLogin);
            RegisterCommand = new DelegateCommand<LoginView>(OnRegister);
            EnterAsGuestCommand = new DelegateCommand<LoginView>(OnGuest);
        }

        private void OnGuest(LoginView obj)
        {
            GameModule.LoggedUser = null;
            Message = string.Empty;
            obj.DialogResult = true;
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(null);
        }

        private void OnRegister(LoginView obj)
        {
            Message = string.Empty;
            if (obj.pwd.Password.Length < 3)
            {
                Message = "Password must be atleast 3 characters";
                return;
            }
            var user = User.GetUser(Username);
            if (user == null)
            {
                GameModule.LoggedUser = User.NewUser(Username, obj.pwd.Password);
                obj.DialogResult = true;
            }
            else
            {
                Message = "Username already used";
            }
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(null);
        }

        private void OnLogin(LoginView obj)
        {
            Message = string.Empty;
            var user = User.GetUser(Username);
            if (user != null && user.Password == obj.pwd.Password)
            {
                GameModule.LoggedUser = user;
                obj.DialogResult = true;
            }
            else
            {
                Message = "Wrong username or password";
            }
            _eventAggregator.GetEvent<RefreshTableEvent>().Publish(null);
        }
    }
}
