using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure.Enums;
using FenService.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace FenService.Tests
{
    public class FenServiceFixture
    {
        private FenService _sut;

        public FenServiceFixture()
        {
            _sut = new FenService();
        }

        [Fact]
        public void GetFen_Should_ReturnCorrectFenNotation_BasedOnFenData()
        {
            // arrange
            var fenData = GetTestFenData();

            // act
            var fenNotation = _sut.GetFen(fenData);

            // assert
            Assert.Equal("rn1qkb2/1pp1pprp/5np1/p2pN1B1/P2P2bP/8/1PPQPPP1/R3KBNR b KQkq - 0 1", fenNotation);
        }

        [Fact]
        public void GetData_Should_ReturnCorrectFenData_BasedOnFenNotation()
        {
            // arrange
            var fenNotation = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

            // act
            var fenData = _sut.GetData(fenNotation);

            // assert
            Assert.Equal(PieceColor.White, fenData.ColorToMove);
        }

        private FenData GetTestFenData()
        {
            var fenData = new FenData();

            var pieces = new List<IPiece>();
            pieces.Add(Helper.GetMockedPiece(PieceType.Rook, PieceColor.White, 1, 'a'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Rook, PieceColor.White, 1, 'h'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Knight, PieceColor.White, 5, 'e'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Knight, PieceColor.White, 1, 'g'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Bishop, PieceColor.White, 5, 'g'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Bishop, PieceColor.White, 1, 'f'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Queen, PieceColor.White, 2, 'd'));
            pieces.Add(Helper.GetMockedPiece(PieceType.King, PieceColor.White, 1, 'e'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 4, 'a'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 2, 'b'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 2, 'c'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 4, 'd'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 2, 'e'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 2, 'f'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 2, 'g'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.White, 4, 'h'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Rook, PieceColor.Black, 8, 'a'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Rook, PieceColor.Black, 7, 'g'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Knight, PieceColor.Black, 8, 'b'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Knight, PieceColor.Black, 6, 'f'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Bishop, PieceColor.Black, 8, 'f'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Bishop, PieceColor.Black, 4, 'g'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Queen, PieceColor.Black, 8, 'd'));
            pieces.Add(Helper.GetMockedPiece(PieceType.King, PieceColor.Black, 8, 'e'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 5, 'a'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 7, 'b'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 7, 'c'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 5, 'd'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 7, 'e'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 7, 'f'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 6, 'g'));
            pieces.Add(Helper.GetMockedPiece(PieceType.Pawn, PieceColor.Black, 7, 'h'));

            fenData.Pieces = pieces.ToArray();
            fenData.ColorToMove = PieceColor.Black;
            return fenData;
        }
    }
}
