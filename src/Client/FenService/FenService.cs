using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using FenService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FenService
{
    public class FenService : IFenService
    {
        private const int FileSection = 0;
        private const int ColorToMoveSection = 1;

        public string GetFen(FenData fenData)
        {
            var sb = new StringBuilder();
            for (int rank = 8; rank > 0; rank--)
            {
                var whiteSpaceCounter = 0;
                for (char file = 'a'; file <= 'h'; file++)
                {
                    var piece = fenData.PieceInfos.FirstOrDefault(p => p.File == file && p.Rank == rank);
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
                if (rank != 1)
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
            var pieces = new List<PieceInfo>();
            var fenData = new FenData();
            var fenSections = fen.Split(' ');
            var allPieces = fenSections[FileSection].Split('/');
            for (int pieceGroup = 0; pieceGroup < 8; pieceGroup++)
            {
                var rank = 8 - pieceGroup;
                var rankPieces = allPieces[pieceGroup];
                var file = 'a';
                for (int j = 0; j < rankPieces.Length; j++)
                {
                    var filePiece = rankPieces[j];
                    if (char.IsNumber(rankPieces, j))
                    {
                        file += (char)Convert.ToInt16(rankPieces[j].ToString());
                    }
                    else
                    {
                        pieces.Add(GetPiece(filePiece, rank, file));
                        file++;
                    }
                }
            }
            fenData.PieceInfos = pieces.ToArray();
            fenData.ColorToMove = GetColorToMove(fenSections[ColorToMoveSection][0]);
            return fenData;
        }

        private PieceInfo GetPiece(char filePiece, int rank, char file)
        {
            return new PieceInfo
            {
                Rank = rank,
                File = file,
                Color = GetColor(filePiece),
                Type = GetType(filePiece)
            };
        }

        private PieceType GetType(char filePiece)
        {
            switch (char.ToLower(filePiece))
            {
                case 'p':
                    return PieceType.Pawn;
                case 'r':
                    return PieceType.Rook;
                case 'n':
                    return PieceType.Knight;
                case 'b':
                    return PieceType.Bishop;
                case 'q':
                    return PieceType.Queen;
                case 'k':
                    return PieceType.King;
            }
            throw new FormatException();
        }

        private PieceColor GetColor(char filePiece)
        {
            return char.IsUpper(filePiece) ? PieceColor.White : PieceColor.Black;
        }

        private PieceColor GetColorToMove(char colorChar)
        {
            return colorChar == 'w' ? PieceColor.White : PieceColor.Black;
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

        private string GetFenRepresentation(PieceInfo pieceInfo)
        {
            var repr = GetPieceRepresentation(pieceInfo.Type);
            return pieceInfo.Color == PieceColor.White ? repr.ToUpper() : repr;
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
