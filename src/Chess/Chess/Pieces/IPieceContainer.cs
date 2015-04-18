using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Pieces
{
    public interface IPieceContainer : IEnumerable<IPiece>
    {
        IEnumerable<IPiece> Pieces { get; }
        void Add(IPiece piece);
        void Remove(IPiece piece);
        void ForEach(Action<IPiece> action);
        bool IsFree(int x, int y);
        bool IsFree(Position pos);
        bool IsOccupied(int x, int y);
        bool IsOccupied(Position pos);
        IPiece this[Position pos] { get; }
    }
}
