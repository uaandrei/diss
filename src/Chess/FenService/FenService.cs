using Chess.Infrastructure.Enums;
using FenService.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace FenService
{
    public class FenService : IFenService
    {
        public string GetFen(FenData fenData)
        {
            var sb = new StringBuilder();
            for (int y = 8; y > 0; y--)
            {
                var whiteSpaceCounter = 0;
                for (char x = 'a'; x <= 'h'; x++)
                {
                    var piece = fenData.Pieces.FirstOrDefault(p => p.File == x && p.Rank == y);
                    if (piece == null)
                        whiteSpaceCounter++;
                    else
                    {
                        if (whiteSpaceCounter > 0)
                        {
                            sb.Append(whiteSpaceCounter);
                            whiteSpaceCounter = 0;
                        }
                        sb.Append(GetFenRepresentation(piece));
                    }
                }
                if (whiteSpaceCounter > 0)
                {
                    sb.Append(whiteSpaceCounter);
                    whiteSpaceCounter = 0;
                }
                if (y != 1)
                    sb.Append("/");
                else
                    sb.Append(" ");
            }
            sb.Append(GetPlayerToMoveRepresentation(fenData.ColorToMove));
            sb.Append(" KQkq");//castling
            sb.Append(" -");//en pessant
            sb.Append(" 0");//half moves <<= relevant for fifty moves rule
            sb.Append(" 1");//full moves <<= The number of the full move. It starts at 1, and is incremented after Black's move
            return sb.ToString();
        }

        public FenData GetData(string fen)
        {
            throw new NotImplementedException();
        }

        private string GetPlayerToMoveRepresentation(PieceColor pieceColor)
        {
            switch (pieceColor)
            {
                case Chess.Infrastructure.Enums.PieceColor.Black:
                    return "b";
                case Chess.Infrastructure.Enums.PieceColor.White:
                    return "w";
            }
            throw new FormatException();
        }

        private string GetFenRepresentation(Chess.Business.Interfaces.Piece.IPiece piece)
        {
            var repr = GetPieceRepresentation(piece.Type);
            return piece.Color == PieceColor.White ? repr.ToUpper() : repr;
        }

        private string GetPieceRepresentation(PieceType type)
        {
            switch (type)
            {
                case Chess.Infrastructure.Enums.PieceType.Pawn:
                    return "p";
                case Chess.Infrastructure.Enums.PieceType.Rook:
                    return "r";
                case Chess.Infrastructure.Enums.PieceType.Knight:
                    return "n";
                case Chess.Infrastructure.Enums.PieceType.Bishop:
                    return "b";
                case Chess.Infrastructure.Enums.PieceType.Queen:
                    return "q";
                case Chess.Infrastructure.Enums.PieceType.King:
                    return "k";
            }
            throw new FormatException();
        }
    }
}
