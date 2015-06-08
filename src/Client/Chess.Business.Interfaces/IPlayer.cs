using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using Chess.Infrastructure.Enums;
using System.Collections.Generic;

namespace Chess.Business.Interfaces
{
    public interface IPlayer
    {
        bool IsAutomatic { get; }
        string RequestURI { get; }
        IEnumerable<IPiece> Pieces { get; }
        PieceColor Color { get; }
        int MoveOrder { get; }
        string Name { get; }
        void Move(Position from, Position to);
        bool OwnsPiece(IPiece piece);
        void Act(IGameTable gameTable);
    }
}
