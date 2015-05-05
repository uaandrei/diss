using Chess.Infrastructure.Enums;
using FenService.Interfaces;
using Moq;

namespace FenService.Tests
{
    static class Helper
    {
        public static IPieceInfo GetMockedPiece(PieceType type, PieceColor color, int rank, char file)
        {
            return new PieceInfo
            {
                Color = color,
                File = file,
                Rank = rank,
                Type = type
            };
        }
    }
}
