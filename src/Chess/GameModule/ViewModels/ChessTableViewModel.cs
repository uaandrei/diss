using System.Linq;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;

namespace Chess.Game.ViewModels
{
    public class ChessTableViewModel
    {
        private GameFactory _gameFactory;
        public ObservableCollection<ChessSquareViewModel> Squares { get; set; }

        public ChessTableViewModel()
        {
            _gameFactory = new GameFactory();
            _gameFactory.Initialize();
            SetupTable();
        }

        private void SetupTable()
        {
            var whitePieces = _gameFactory.Pieces.Take(16).ToList();
            var blackPieces = _gameFactory.Pieces.Except(whitePieces).ToList();
            var squares = new List<ChessSquareViewModel>();

            foreach (var piece in whitePieces)
            {
                squares.Add(new ChessSquareViewModel(8 * piece.CurrentPosition.Y + piece.CurrentPosition.X, piece));
            }
            for (int i = 16; i <= 47; i++)
            {
                squares.Add(new ChessSquareViewModel(i));
            }
            foreach (var piece in blackPieces)
            {
                squares.Add(new ChessSquareViewModel(8 * piece.CurrentPosition.Y + piece.CurrentPosition.X, piece));
            }
            Squares = new ObservableCollection<ChessSquareViewModel>(squares.OrderByDescending(p => p.Index).ToList());
        }
    }
}