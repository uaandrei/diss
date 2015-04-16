using Chess.Infrastructure.Enums;
using Chess.Pieces;

namespace Chess.Infrastructure.Behaviours
{
    public interface IChessSquareViewModel
    {
        string Representation { get; }
        IPiece Piece { get; }
        int Index { get; }
        SquareStates SquareState { get; set; }
    }
}
