using Chess.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Infrastructure
{
    public static class Helper
    {
        public static PieceType GetType(char filePiece)
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
    }
}
