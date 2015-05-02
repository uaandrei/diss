using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure.Enums;
using Moq;
namespace FenService.Tests
{
    static class Helper
    {
        public static IPiece GetMockedPiece(PieceType type, PieceColor color, int rank, char file)
        {
            var mockedPiece = new Mock<IPiece>();
            mockedPiece
                .SetupGet(p => p.Rank)
                .Returns(rank);
            mockedPiece
                .SetupGet(p => p.File)
                .Returns(file);
            mockedPiece
                .SetupGet(p => p.Color)
                .Returns(color);
            mockedPiece
                .SetupGet(p => p.Type)
                .Returns(type);
            return mockedPiece.Object;
        }
    }
}
