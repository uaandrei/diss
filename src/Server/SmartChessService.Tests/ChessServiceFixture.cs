using Xunit;
namespace SmartChessService.Tests
{
    public class ChessServiceFixture
    {
        private ChessService _sut;

        public ChessServiceFixture()
        {
            _sut = new ChessService();
        }

        [Fact]
        public void GetMoveResponse_ShouldReturnMove_g3g6WithScore_28997()
        {
            // arrange
            var fen = "2rr3k/pp3pp1/1nnqbN1p/3pN3/2pP4/2P3Q1/PPB4P/R4RK1 w - -";
            var depth = 6;
            var expectedResponse = "g3g6;28997";

            // act
            var actualResponse = _sut.GetMoveResponse(fen, depth);

            // assert
            Assert.Equal(expectedResponse, actualResponse);
        }
    }
}
