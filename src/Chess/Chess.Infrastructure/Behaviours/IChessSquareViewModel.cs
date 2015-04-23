using Chess.Infrastructure.Enums;

namespace Chess.Infrastructure.Behaviours
{
    public interface IChessSquareViewModel
    {
        string Representation { get; set; }
        SquareState SquareState { get; set; }
        Position Position { get; }
    }
}
