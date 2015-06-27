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
        private const int CastlingSection = 2;
        private const int EnPassantSection = 3;
        private const int HalfMovesSection = 4;
        private const int FullMovesSection = 5;

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
            sb.Append(GetPlayerToMoveRepresentation(fenData.GameInfo.ColorToMove));
            sb.Append(" ");
            AppendCharacterIfTrue(sb, () => fenData.GameInfo.Wkca, "K");
            AppendCharacterIfTrue(sb, () => fenData.GameInfo.Wqca, "Q");
            AppendCharacterIfTrue(sb, () => fenData.GameInfo.Bkca, "k");
            AppendCharacterIfTrue(sb, () => fenData.GameInfo.Bqca, "q");
            sb.Append(" ");
            AppendCharacterIfTrue(sb, () => fenData.GameInfo.EnPassant != null, fenData.GameInfo.EnPassant != null ? fenData.GameInfo.EnPassant.ToAlgebraic() : "-");
            sb.Append(" ");
            AppendCharacterIfTrue(sb, () => true, fenData.GameInfo.HalfMoves.ToString());
            sb.Append(" ");
            AppendCharacterIfTrue(sb, () => true, fenData.GameInfo.FullMoves.ToString());
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
            fenData.GameInfo.ColorToMove = GetColorToMove(fenSections[ColorToMoveSection][0]);
            HandleCastlingPermissions(fenData.GameInfo, fenSections[CastlingSection]);
            HandleEnPassantSquare(fenData.GameInfo, fenSections[EnPassantSection]);
            fenData.GameInfo.HalfMoves = Convert.ToInt32(fenSections[HalfMovesSection]);
            fenData.GameInfo.FullMoves = Convert.ToInt32(fenSections[FullMovesSection]);
            return fenData;
        }

        private void HandleEnPassantSquare(GameInfo gameInfo, string enPas)
        {
            if (enPas != "-")
                gameInfo.EnPassant = new Position(enPas);
        }

        private void HandleCastlingPermissions(GameInfo gameInfo, string castlingPerm)
        {
            if (castlingPerm[0] == 'K')
                gameInfo.Wkca = true;
            if (castlingPerm[1] == 'Q')
                gameInfo.Wqca = true;
            if (castlingPerm[2] == 'k')
                gameInfo.Bkca = true;
            if (castlingPerm[3] == 'q')
                gameInfo.Bqca = true;
        }

        private void AppendCharacterIfTrue(StringBuilder apendee, Func<bool> condition, string toApend)
        {
            if (condition())
                apendee.Append(toApend);
            else
                apendee.Append("-");
        }

        private PieceInfo GetPiece(char filePiece, int rank, char file)
        {
            return new PieceInfo
            {
                Rank = rank,
                File = file,
                Color = GetColor(filePiece),
                Type = Helper.GetType(filePiece)
            };
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
