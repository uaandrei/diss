using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Pieces
{
    public class PieceFactory : IPieceFactory
    {
        public List<IPiece> GetPieces()
        {
            var pieces = InitializePieces(PieceColor.Black, 7, 6);
            pieces.AddRange(InitializePieces(PieceColor.White, 0, 1));
            return pieces;
        }

        private List<IPiece> InitializePieces(PieceColor color, int firstLine, int pawnLine)
        {
            var pieces = new List<IPiece>();
            pieces.Add(new ChessPiece(0, firstLine, color, PieceType.Rook));
            pieces.Add(new ChessPiece(1, firstLine, color, PieceType.Knight));
            pieces.Add(new ChessPiece(2, firstLine, color, PieceType.Bishop));
            pieces.Add(new ChessPiece(3, firstLine, color, PieceType.Queen));
            pieces.Add(new ChessPiece(4, firstLine, color, PieceType.King));
            pieces.Add(new ChessPiece(5, firstLine, color, PieceType.Bishop));
            pieces.Add(new ChessPiece(6, firstLine, color, PieceType.Knight));
            pieces.Add(new ChessPiece(7, firstLine, color, PieceType.Rook));
            for (int i = 0; i < 8; i++)
            {
                pieces.Add(new ChessPiece(i, pawnLine, color, PieceType.Pawn));
            }
            return pieces;
        }
    }
}
