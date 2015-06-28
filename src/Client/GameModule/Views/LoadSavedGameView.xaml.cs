using Chess.Game.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess.Game.Views
{
    /// <summary>
    /// Interaction logic for LoadSavedGameView.xaml
    /// </summary>
    public partial class LoadSavedGameView : Window, IView<ILoadSavedGameViewModel>
    {
        public LoadSavedGameView()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
        }

        [Dependency]
        public ILoadSavedGameViewModel ViewModel
        {
            get { return DataContext as ILoadSavedGameViewModel; }
            set { DataContext = value; }
        }

        public bool? ShowView()
        {
            return ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            if (listView.SelectedItems != null && listView.SelectedItems.Count > 0)
                DialogResult = true;
        }
    }
}
