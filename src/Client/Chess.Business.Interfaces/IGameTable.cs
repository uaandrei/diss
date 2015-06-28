using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.Interfaces
{
    public interface IGameTable
    {
        IPlayer CurrentPlayer { get; }
        IEnumerable<IPlayer> Players { get; }
        IEnumerable<Position> TableMoves { get; }
        IEnumerable<Position> TableAttacks { get; }
        Position SelectedSquare { get; }
        int Difficulty { get; set; }
        string Id { get; }
        IEnumerable<IPiece> GetPieces();
        void StartNewGame();
        string GetFen();
        void LoadFromFen(string fen, bool clearStack = true);
        void UndoLastMove();
        void ParseInput(Position userInput);
        void ChangePlayers(bool isBlackAI, bool isWhiteAI);
        void SetSelectedPiece(Position piecePosition);
    }
}
