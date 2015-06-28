using Chess.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game.ViewModels
{
    public class PromotionViewModel : ViewModelBase, IPromotionViewModel
    {
        private string _pieceTypeName;
        public string PieceTypeName
        {
            get { return _pieceTypeName; }
            set
            {
                _pieceTypeName = value;
                NotifyPropertyChanged();
            }
        }

        public PieceType PieceType
        {
            get { return (PieceType)Enum.Parse(typeof(PieceType), PieceTypeName); }
        }

        public PromotionViewModel()
        {
            PieceTypeName = PieceType.Queen.ToString();
        }
    }
}
