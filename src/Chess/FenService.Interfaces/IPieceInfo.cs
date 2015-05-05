using Chess.Infrastructure.Enums;

namespace FenService.Interfaces
{
    public interface IPieceInfo
    {
        PieceColor Color { get; }
        PieceType Type { get; }
        int Rank { get; }
        char File { get; }
    }
}
