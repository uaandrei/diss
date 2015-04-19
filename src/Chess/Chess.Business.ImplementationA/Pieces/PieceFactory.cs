using Chess.Business.ImplementationA.Moves;
using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Pieces
{
    public class PieceFactory : IPieceFactory
    {
        private IPieceContainer _container;
        public IList<IPiece> Pieces { get; private set; }

        public PieceFactory()
        {
            Pieces = new List<IPiece>();
        }

        public void Initialize(IPieceContainer container)
        {
            // TODO: fix this??!?!?@#$E%@#$#!@
            _container = container;
            InitializePieces(PieceColor.White, 7, 6);
            InitializePieces(PieceColor.Black, 0, 1);
        }

        private void InitializePieces(PieceColor color, int firstLine, int secondLine)
        {
            Pieces.Add(new ChessPiece(0, firstLine, color, PieceType.Rook, new OrthogonalMove(_container)));
            Pieces.Add(new ChessPiece(1, firstLine, color, PieceType.Knight, new LMove(_container)));
            Pieces.Add(new ChessPiece(2, firstLine, color, PieceType.Bishop, new DiagonalMove(_container)));
            Pieces.Add(new ChessPiece(3, firstLine, color, PieceType.Queen, new CompositeMove(new OrthogonalMove(_container), new DiagonalMove(_container))));
            Pieces.Add(new ChessPiece(4, firstLine, color, PieceType.King, new SquareMove(_container)));
            Pieces.Add(new ChessPiece(5, firstLine, color, PieceType.Bishop, new DiagonalMove(_container)));
            Pieces.Add(new ChessPiece(6, firstLine, color, PieceType.Knight, new LMove(_container)));
            Pieces.Add(new ChessPiece(7, firstLine, color, PieceType.Rook, new OrthogonalMove(_container)));
            for (int i = 0; i < 8; i++)
            {
                if (color == PieceColor.Black)
                    Pieces.Add(new ChessPiece(i, secondLine, color, PieceType.Pawn, new BlackPawnMove(_container)));
                else
                    Pieces.Add(new ChessPiece(i, secondLine, color, PieceType.Pawn, new WhitePawnMove(_container)));
            }
        }
    }
}
