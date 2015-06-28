using Chess.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.ViewModels
{
    public interface IPromotionViewModel
    {
        PieceType PieceType { get; }
    }
}
