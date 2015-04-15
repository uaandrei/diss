using Chess.Moves;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class GameFactory
    {
        private int[,] _matrix;
        public List<IPiece> Pieces { get; private set; }

        public GameFactory()
        {
            InitializeTable();
        }

        public void Initialize()
        {
            InitializePieces(PieceColor.White, 7, 6);
            InitializePieces(PieceColor.Black, 0, 1);
            SetupGame();
        }

        private void InitializeTable()
        {
            _matrix = new int[8, 8];
            Pieces = new List<IPiece>();
        }

        private void InitializePieces(PieceColor color, int firstLine, int secondLine)
        {
            Pieces.Add(new ChessPiece(0, firstLine, color, PieceType.Rook, new OrthogonalMove(_matrix)));
            Pieces.Add(new ChessPiece(1, firstLine, color, PieceType.Knight, new LMove(_matrix)));
            Pieces.Add(new ChessPiece(2, firstLine, color, PieceType.Bishop, new DiagonalMove(_matrix)));
            Pieces.Add(new ChessPiece(3, firstLine, color, PieceType.Queen, new CompositeMove(new OrthogonalMove(_matrix), new DiagonalMove(_matrix))));
            Pieces.Add(new ChessPiece(4, firstLine, color, PieceType.King, new SquareMove(_matrix)));
            Pieces.Add(new ChessPiece(5, firstLine, color, PieceType.Bishop, new DiagonalMove(_matrix)));
            Pieces.Add(new ChessPiece(6, firstLine, color, PieceType.Knight, new LMove(_matrix)));
            Pieces.Add(new ChessPiece(7, firstLine, color, PieceType.Rook, new OrthogonalMove(_matrix)));
            _matrix[0, firstLine] = 2;
            _matrix[1, firstLine] = 3;
            _matrix[2, firstLine] = 4;
            _matrix[3, firstLine] = 10;
            _matrix[4, firstLine] = 999;
            _matrix[5, firstLine] = 4;
            _matrix[6, firstLine] = 3;
            _matrix[7, firstLine] = 2;
            for (int i = 0; i < 8; i++)
            {
                Pieces.Add(new ChessPiece(i, secondLine, color, PieceType.Pawn, new PawnMove(_matrix, color)));
                _matrix[i, secondLine] = 1;
            }
        }

        private void SetupGame()
        {
        }
    }
}
