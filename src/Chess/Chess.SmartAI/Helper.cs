using FenService.Interfaces;

namespace Chess.SmartAI
{
    static class Helper
    {
        public static int GetPieceSquare(IPieceInfo pieceInfo)
        {
            var x = pieceInfo.Rank - 1;
            var y = pieceInfo.File - 97;
            return x * 8 + y;
        }

        public static string GetPieceString(IPieceInfo pieceInfo)
        {
            var prefix = pieceInfo.Color == Infrastructure.Enums.PieceColor.Black ? "B" : "W";
            var pieceType = string.Empty;
            switch (pieceInfo.Type)
            {
                case Chess.Infrastructure.Enums.PieceType.Empty:
                    break;
                case Chess.Infrastructure.Enums.PieceType.Pawn:
                    pieceType = "P";
                    break;
                case Chess.Infrastructure.Enums.PieceType.Rook:
                    pieceType = "R";
                    break;
                case Chess.Infrastructure.Enums.PieceType.Knight:
                    pieceType = "N";
                    break;
                case Chess.Infrastructure.Enums.PieceType.Bishop:
                    pieceType = "B";
                    break;
                case Chess.Infrastructure.Enums.PieceType.Queen:
                    pieceType = "Q";
                    break;
                case Chess.Infrastructure.Enums.PieceType.King:
                    pieceType = "K";
                    break;
                default:
                    break;
            }
            return prefix + pieceType;
        }
    }
}
