using Chess.Infrastructure;
using Chess.Infrastructure.Enums;

namespace Chess.Game.ViewModels
{
    public interface IChessSquareViewModel
    {
        string Representation { get; set; }
        SquareState SquareState { get; set; }
        Position Position { get; }
    }
}
