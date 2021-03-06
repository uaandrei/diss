﻿using Chess.Game.ViewModels;
using Microsoft.Practices.Unity;
using System.Windows.Controls;

namespace Chess.Game.Views
{
    /// <summary>
    /// Interaction logic for ChessTableView.xaml
    /// </summary>
    public partial class ChessTableView : UserControl
    {
        public ChessTableView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IChessTableViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
    }
}
