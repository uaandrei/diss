using System;
namespace Chess.SmartAI {
	public class jcAISearchAgentAlphabeta : jcAISearchAgent {
		// Construction
		public jcAISearchAgentAlphabeta()
			: base() {
		}

		// jcMove PickBestMove
		// Implementation of the abstract method defined in the superclass
		public override jcMove PickBestMove(jcBoard theBoard) {
			// Store the identity of the moving side, so that we can tell Evaluator
			// from whose perspective we need to evaluate positions
			FromWhosePerspective = theBoard.GetCurrentPlayer();
			MoveCounter++;

			// Should we erase the history table?
			if((Rnd.Next() % 4) == 2)
				HistoryTable.Forget();

			NumRegularNodes = 0;
			NumQuiescenceNodes = 0;
			NumRegularTTHits = 0;
			NumQuiescenceTTHits = 0;

			// Find the moves
			jcMove theMove = new jcMove();
			jcMoveListGenerator movegen = new jcMoveListGenerator();
			movegen.ComputeLegalMoves(theBoard);
			HistoryTable.SortMoveList(movegen, theBoard.GetCurrentPlayer());

			// The following code blocks look a lot like the MAX node case from
			// jcAISearchAgent.Alphabeta, with an added twist: we need to store the
			// actual best move, and not only pass around its minimax value
			int bestSoFar = ALPHABETA_MINVAL;
			jcBoard newBoard = new jcBoard();
			jcMove mov;
			int currentAlpha = ALPHABETA_MINVAL;

			// Loop on all pseudo-legal moves
			while((mov = movegen.Next()) != null) {
				newBoard.Clone(theBoard);
				newBoard.ApplyMove(mov);
				int movScore = AlphaBeta(MINNODE, newBoard, 5, currentAlpha, ALPHABETA_MAXVAL);
				if(movScore == ALPHABETA_ILLEGAL)
					continue;

				currentAlpha = Math.Max(currentAlpha, movScore);

				if(movScore > bestSoFar) {
					theMove.Copy(mov);
					bestSoFar = movScore;
					theMove.MoveEvaluation = movScore;
				}
			}

			// And now, if the best we can do is ALPHABETA_GIVEUP or worse, then it is
			// time to resign...  Unless the opponent was kind wnough to put us in
			// stalemate!
			if(bestSoFar <= ALPHABETA_GIVEUP) {
				// Check for a stalemate
				// Stalemate occurs if the player's king is NOT in check, but all of his
				// moves are illegal.
				// First, verify whether we are in check
				newBoard.Clone(theBoard);
				jcMoveListGenerator secondary = new jcMoveListGenerator();
				newBoard.SwitchSides();
				if(secondary.ComputeLegalMoves(newBoard)) {
					// Then, we are not in check and may continue our efforts.
					// We must now examine all possible moves; if at least one resuls in
					// a legal position, there is no stalemate and we must assume that
					// we are doomed
					HistoryTable.SortMoveList(movegen, newBoard.GetCurrentPlayer());
					movegen.ResetIterator();
					// If we can scan all moves without finding one which results
					// in a legal position, we have a stalemate
					theMove.MoveType = jcMove.MOVE_STALEMATE;
					theMove.MovingPiece = jcBoard.KING + theBoard.GetCurrentPlayer();
					// Look at the moves
					while((mov = movegen.Next()) != null) {
						newBoard.Clone(theBoard);
						newBoard.ApplyMove(mov);
						if(secondary.ComputeLegalMoves(newBoard)) {
							theMove.MoveType = jcMove.MOVE_RESIGN;
						}
					}
				} else {
					// We're in check and our best hope is GIVEUP or worse, so either we are
					// already checkmated or will be soon, without hope of escape
					theMove.MovingPiece = jcBoard.KING + theBoard.GetCurrentPlayer();
					theMove.MoveType = jcMove.MOVE_RESIGN;
				}
			}

			Console.Write("  --> Transposition Table hits for regular nodes: ");
			Console.WriteLine(NumRegularTTHits + " of " + NumRegularNodes);
			Console.Write("  --> Transposition Table hits for quiescence nodes: ");
			Console.WriteLine(NumQuiescenceTTHits + " of " + NumQuiescenceNodes);

			return theMove;
		}
	}
}
