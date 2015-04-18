using Chess.Infrastructure.Enums;
using Chess.Pieces;

namespace Chess.Infrastructure.Behaviours
{
    public interface IChessSquareViewModel
    {
        string Representation { get; set; }
        SquareState SquareState { get; set; }
        Position Position { get; }
    }
}
