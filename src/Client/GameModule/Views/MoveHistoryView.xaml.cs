using Chess.Game.ViewModels;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Interaction logic for MoveHistoryView.xaml
    /// </summary>
    public partial class MoveHistoryView : UserControl
    {
        public MoveHistoryView()
        {
            InitializeComponent();
            (Moves.Items as INotifyCollectionChanged).CollectionChanged += MoveHistoryView_CollectionChanged;
        }

        void MoveHistoryView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Moves.Items.Count > 0)
                Moves.ScrollIntoView(Moves.Items[Moves.Items.Count - 1]);
        }

        [Dependency]
        public IMoveHistoryViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
    }
}
