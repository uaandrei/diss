using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.ViewModels
{
    public class OptionsViewModel : ViewModelBase, IOptionsViewModel
    {
        public bool IsBlackAI { get; set; }
        public bool IsWhiteAI { get; set; }
        public int Difficulty { get; set; }
    }
}
