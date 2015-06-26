using Chess.Game.ViewModels;
using Microsoft.Practices.Unity;
using System.Windows.Controls;

namespace Chess.Game.Views
{
    /// <summary>
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : UserControl
    {
        public NotificationView()
        {
            InitializeComponent();
        }

        [Dependency]
        public INotificationViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
    }
}
