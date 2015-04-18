using Chess.Pieces;
using Moq;
using System.Collections.Generic;
namespace Chess.Tests
{
    static class Helper
    {
        public static IPieceContainer GetEmptyContainer()
        {
            var mockedEmptyFactory = new Mock<IPieceFactory>();
            mockedEmptyFactory
                .Setup(f => f.Pieces)
                .Returns(new List<IPiece>());
            return new PieceContainer(mockedEmptyFactory.Object);
        }

        public static IPiece GetMockedPieceAt(int x, int y)
        {
            var mockedPiece = new Mock<IPiece>();
            mockedPiece
                .SetupGet(p => p.CurrentPosition)
                .Returns(new Position(x, y));
            return mockedPiece.Object;
        }
    }
}
