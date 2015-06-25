using Chess.Business.Interfaces;
using Chess.Infrastructure.Enums;
using System;
using System.Linq;

namespace Chess.Business.ImplementationA.Rules
{
    public class ChessRule : IRule
    {
        private IGameTable _gameTable;

        public ChessRule(IGameTable gameTable)
        {
            _gameTable = gameTable;
        }

        public bool IsTrue()
        {
            var king = _gameTable.CurrentPlayer.Pieces.First(p => p.Type == PieceType.King);
            var opponent = _gameTable.Players.First(p => p.Color != king.Color);
            foreach (var piece in opponent.Pieces)
            {
                var pieceAttacks = piece.GetAvailableAttacks(_gameTable.GetPieces());
                var anyPieceAttacksKing = pieceAttacks.Any(p => p == king.CurrentPosition);
                if (anyPieceAttacksKing)
                    return true;
            }
            return false;
        }
    }
}
