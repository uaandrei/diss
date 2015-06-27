using Chess.Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.Views
{
    public interface IOptionsView
    {
        IOptionsViewModel ViewModel { get; }
    }
}
