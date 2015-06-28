using System.Linq;
using Chess.Infrastructure.Enums;
using FenService.Interfaces;
using System.Collections.Generic;
using Xunit;
using Chess.Infrastructure;

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
            Assert.Equal("rn1qkb2/1pp1pprp/5np1/p2pN1B1/P2P2bP/8/1PPQPPP1/R3KBNR b K--q a3 12 45", fenNotation);
        }

        [Fact]
        public void GetData_Should_ReturnCorrectFenData_BasedOnFenNotation()
        {
            // arrange
            var fenNotation = "rnbqkbnr/ppp1pppp/8/3p4/8/5N2/PPPPPPPP/RNBQKB1R w -Qk- f6 11 30";

            // act
            var fenData = _sut.GetData(fenNotation);

            // assert
            for (char file = 'a'; file <= 'h'; file++)
            {
                fenData.AssertSinglePiece(2, file, PieceType.Pawn, PieceColor.White);
                if (file == 'd')
                    continue;
                fenData.AssertSinglePiece(7, file, PieceType.Pawn, PieceColor.Black);
            }
            fenData.AssertSinglePiece(5, 'd', PieceType.Pawn, PieceColor.Black);
            fenData.AssertSinglePiece(1, 'a', PieceType.Rook, PieceColor.White);
            fenData.AssertSinglePiece(1, 'b', PieceType.Knight, PieceColor.White);
            fenData.AssertSinglePiece(1, 'c', PieceType.Bishop, PieceColor.White);
            fenData.AssertSinglePiece(1, 'd', PieceType.Queen, PieceColor.White);
            fenData.AssertSinglePiece(1, 'e', PieceType.King, PieceColor.White);
            fenData.AssertSinglePiece(1, 'f', PieceType.Bishop, PieceColor.White);
            fenData.AssertSinglePiece(3, 'f', PieceType.Knight, PieceColor.White);
            fenData.AssertSinglePiece(1, 'h', PieceType.Rook, PieceColor.White);
            fenData.AssertSinglePiece(8, 'a', PieceType.Rook, PieceColor.Black);
            fenData.AssertSinglePiece(8, 'b', PieceType.Knight, PieceColor.Black);
            fenData.AssertSinglePiece(8, 'c', PieceType.Bishop, PieceColor.Black);
            fenData.AssertSinglePiece(8, 'd', PieceType.Queen, PieceColor.Black);
            fenData.AssertSinglePiece(8, 'e', PieceType.King, PieceColor.Black);
            fenData.AssertSinglePiece(8, 'f', PieceType.Bishop, PieceColor.Black);
            fenData.AssertSinglePiece(8, 'g', PieceType.Knight, PieceColor.Black);
            fenData.AssertSinglePiece(8, 'h', PieceType.Rook, PieceColor.Black);
            Assert.Equal(PieceColor.White, fenData.GameInfo.ColorToMove);
            Assert.False(fenData.GameInfo.Wkca);
            Assert.True(fenData.GameInfo.Wqca);
            Assert.True(fenData.GameInfo.Bkca);
            Assert.False(fenData.GameInfo.Bqca);
            Assert.Equal(new Position(6, 'f'), fenData.GameInfo.EnPassant);
            Assert.Equal(11, fenData.GameInfo.HalfMoves);
            Assert.Equal(30, fenData.GameInfo.FullMoves);
        }

        private FenData GetTestFenData()
        {
            var fenData = new FenData();

            var pieces = new List<PieceInfo>();
            pieces.Add(Helper.GetPieceInfo(PieceType.Rook, PieceColor.White, 1, 'a'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Rook, PieceColor.White, 1, 'h'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Knight, PieceColor.White, 5, 'e'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Knight, PieceColor.White, 1, 'g'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Bishop, PieceColor.White, 5, 'g'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Bishop, PieceColor.White, 1, 'f'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Queen, PieceColor.White, 2, 'd'));
            pieces.Add(Helper.GetPieceInfo(PieceType.King, PieceColor.White, 1, 'e'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 4, 'a'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 2, 'b'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 2, 'c'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 4, 'd'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 2, 'e'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 2, 'f'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 2, 'g'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.White, 4, 'h'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Rook, PieceColor.Black, 8, 'a'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Rook, PieceColor.Black, 7, 'g'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Knight, PieceColor.Black, 8, 'b'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Knight, PieceColor.Black, 6, 'f'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Bishop, PieceColor.Black, 8, 'f'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Bishop, PieceColor.Black, 4, 'g'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Queen, PieceColor.Black, 8, 'd'));
            pieces.Add(Helper.GetPieceInfo(PieceType.King, PieceColor.Black, 8, 'e'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 5, 'a'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 7, 'b'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 7, 'c'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 5, 'd'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 7, 'e'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 7, 'f'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 6, 'g'));
            pieces.Add(Helper.GetPieceInfo(PieceType.Pawn, PieceColor.Black, 7, 'h'));

            fenData.PieceInfos = pieces.ToArray();
            fenData.GameInfo.ColorToMove = PieceColor.Black;
            fenData.GameInfo.Wkca = true;
            fenData.GameInfo.Wqca = false;
            fenData.GameInfo.Bkca = false;
            fenData.GameInfo.Bqca = true;
            fenData.GameInfo.EnPassant = new Position(3, 'a');
            fenData.GameInfo.HalfMoves = 12;
            fenData.GameInfo.FullMoves = 45;
            return fenData;
        }
    }

    public static class FenServiceFixtureExtension
    {
        public static void AssertSinglePiece(this FenData fenData, int rank, char file, PieceType type, PieceColor color)
        {
            Assert.Single(fenData.PieceInfos, p => p.Rank == rank && p.File == file && p.Type == type && p.Color == color);
        }
    }
}
