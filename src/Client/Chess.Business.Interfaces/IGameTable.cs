using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.Interfaces
{
    public interface IGameTable
    {
        IPlayer CurrentPlayer { get; }
        IEnumerable<Position> TableMoves { get; }
        IEnumerable<Position> TableAttacks { get; }
        Position SelectedSquare { get; }
        Position MovedTo { get; }
        int Difficulty { get; }
        IEnumerable<IPiece> GetPieces();
        void StartNewGame();
        string GetFen();
        void LoadFromFen(string fen);
        void UndoLastMove();
        void ParseInput(Position userInput);
    }
}
