﻿using Chess.Business.Interfaces.Piece;
using Chess.Infrastructure;
using System.Collections.Generic;

namespace Chess.Business.ImplementationA.Moves
{
    public class DiagonalMove : ContinousConditionedMove
    {
        public override IList<Position> GetMoves(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), currentPiece, allPieces, 1, 1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), currentPiece, allPieces, -1, -1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), currentPiece, allPieces, 1, -1));
            positions.AddRange(GeneratePositions((c, p) => c.IsFree(p), currentPiece, allPieces, -1, 1));
            return positions;
        }

        protected override List<Position> GenerateAttacks(IPiece currentPiece, IEnumerable<IPiece> allPieces)
        {
            var positions = new List<Position>();
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), currentPiece, allPieces, 1, 1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), currentPiece, allPieces, -1, -1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), currentPiece, allPieces, 1, -1));
            positions.AddRange(GeneratePositions((c, p) => !c.IsFree(p), currentPiece, allPieces, -1, 1));
            return positions;
        }
    }
}
