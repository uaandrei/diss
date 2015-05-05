namespace FenService.Interfaces
{
    public interface IFenData
    {
        bool BlackLeftCastling { get; set; }
        bool BlackRightCastling { get; set; }
        Chess.Infrastructure.Enums.PieceColor ColorToMove { get; set; }
        Chess.Infrastructure.Position EnPassant { get; set; }
        int FullMoveNumber { get; set; }
        int HalfMoveClock { get; set; }
        IPieceInfo[] PieceInfos { get; set; }
        bool WhiteLeftCastling { get; set; }
        bool WhiteRightCastling { get; set; }
    }
}
