using Chess.Game.ViewModels;
using Microsoft.Practices.Unity;
using System.Windows;

namespace Chess.Game.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window, IView<ILoginViewModel>
    {
        public LoginView()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
        }

        [Dependency]
        public ILoginViewModel ViewModel
        {
            get { return DataContext as ILoginViewModel; }
            set { DataContext = value; }
        }

        public bool? ShowView()
        {
            return ShowDialog();
        }
    }
}
