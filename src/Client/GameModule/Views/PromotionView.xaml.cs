﻿using Chess.Game.ViewModels;
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
    /// Interaction logic for PromotionView.xaml
    /// </summary>
    public partial class PromotionView : Window, IView<IPromotionViewModel>
    {
        public PromotionView()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        [Dependency]
        public IPromotionViewModel ViewModel
        {
            get { return DataContext as IPromotionViewModel; }
            set { DataContext = value; }
        }

        public bool? ShowView()
        {
            return ShowDialog();
        }
    }
}
