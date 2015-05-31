using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Moq;
using System.Collections.Generic;

namespace Chess.Tests
{
    static class Helper
    {
        public static IPiece GetMockedPieceAt(int x, int y, PieceColor color = PieceColor.Black)
        {
            var mockedPiece = new Mock<IPiece>();
            mockedPiece
                .SetupGet(p => p.CurrentPosition)
                .Returns(new Position(x, y));
            mockedPiece.SetupGet(p => p.Color)
                .Returns(color);
            return mockedPiece.Object;
        }
    }
}
