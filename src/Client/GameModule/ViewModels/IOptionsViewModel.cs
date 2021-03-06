﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.ViewModels
{
    public interface IOptionsViewModel
    {
        bool IsBlackAI { get; set; }
        bool IsWhiteAI { get; set; }
        int Difficulty { get; set; }
    }
}
