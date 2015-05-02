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
        void Start();
        void ParseInput(Position userInput);
        IEnumerable<IPiece> GetPieces();
        string GetFen();
        void LoadFromFen(string fen);
    }
}
