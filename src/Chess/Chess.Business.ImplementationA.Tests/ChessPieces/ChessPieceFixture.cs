using Chess.Business.ImplementationA.Pieces;
using Chess.Business.Interfaces.Move;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using Moq;
using Xunit;

namespace Chess.Tests.ChessPieces
{
    public class ChessPieceFixture
    {
        private ChessPiece _sut;
        private Mock<IMoveStrategy> _moveStrategyMock;

        public ChessPieceFixture()
        {
            _moveStrategyMock = new Mock<IMoveStrategy>();
            _sut = new ChessPiece(new Position(0, 0), PieceColor.Black, PieceType.Bishop, _moveStrategyMock.Object);
        }

        [Fact]
        public void Move_ShouldSetCurrentPosition()
        {
            // arrange
            var newPosition = new Position(3, 4);

            // act 
            _sut.Move(newPosition);

            // assert
            Assert.Equal(newPosition, _sut.CurrentPosition);
        }

        [Fact]
        public void GetMoves_GetAttacks_Verify()
        {
            // arrange
            _moveStrategyMock.Setup(p => p.GetMoves(It.IsAny<Position>()));
            _moveStrategyMock.Setup(p => p.GetAttacks(It.IsAny<Position>()));

            // act
            _sut.GetAvailableMoves();
            _sut.GetAvailableAttacks();

            // assert
            _moveStrategyMock.VerifyAll();
        }
    }
}
