using System;
namespace Chess.SmartAI {
	class jcTranspositionEntry {
		// Data fields, beginning with the actual value of the board and whether this
		// value represents an accurate evaluation or only a boundary
		public int theEvalType;
		public int theEval;

		// This value was obtained through a search to what depth?  0 means that
		// it was obtained during quiescence search (which is always effectively
		// of infinite depth but only within the quiescence domain; full-width
		// search of depth 1 is still more valuable than whatever Qsearch result)
		public int theDepth;

		// Board position signature, used to detect collisions
		public long theLock;

		// What this entry stored so long ago that it may no longer be useful?
		// Without this, the table will slowly become clogged with old, deep search
		// results for positions with no chance of happening again, and new positions
		// (specifically the 0-depth quiescence search positions) will never be
		// stored!
		public int timeStamp;

		public const int NULL_ENTRY = -1;

		// construction
		public jcTranspositionEntry() {
			theEvalType = NULL_ENTRY;
		}
	}


	public class jcTranspositionTable {
		/***************************************************************************
		 * DATA MEMBERS
		 **************************************************************************/

		// The size of a transposition table, in entries
		private const int TABLE_SIZE = 131072;

		// Data
		private jcTranspositionEntry[] Table;

		/**************************************************************************
		 * PUBLIC METHODS
		 *************************************************************************/

		// Construction
		public jcTranspositionTable() {
			Table = new jcTranspositionEntry[TABLE_SIZE];
			for(int i = 0 ; i < TABLE_SIZE ; i++) {
				Table[i] = new jcTranspositionEntry();
			}
		}

		// bool LookupBoard( jcBoard theBoard, jcMove theMove )
		// Verify whether there is a stored evaluation for a given board.
		// If so, return TRUE and copy the appropriate values into the
		// output parameter
		public bool LookupBoard(jcBoard theBoard, jcMove theMove) {
			// Find the board's hash position in Table
			int key = Math.Abs(theBoard.HashKey() % TABLE_SIZE);
			jcTranspositionEntry entry = Table[key];

			// If the entry is an empty placeholder, we don't have a match
			if(entry.theEvalType == -1) // jcTranspositionEntry.NULL_ENTRY )
				return false;

			// Check for a hashing collision!
			if(entry.theLock != theBoard.HashLock())
				return false;

			// Now, we know that we have a match!  Copy it into the output parameter
			// and return
			theMove.MoveEvaluation = entry.theEval;
			theMove.MoveEvaluationType = entry.theEvalType;
			theMove.SearchDepth = entry.theDepth;
			return true;
		}

		// public StoreBoard( theBoard, eval, evalType, depth, timeStamp )
		// Store a good evaluation found through alphabeta for a certain board position
		public bool StoreBoard(jcBoard theBoard, int eval, int evalType, int depth, int timeStamp) {
			int key = Math.Abs(theBoard.HashKey() % TABLE_SIZE);

			// Would we erase a more useful (i.e., higher) position if we stored this
			// one?  If so, don't bother!
			if((Table[key].theEvalType != jcTranspositionEntry.NULL_ENTRY) &&
				 (Table[key].theDepth > depth) &&
				 (Table[key].timeStamp >= timeStamp))
				return true;

			// And now, do the actual work
			Table[key].theLock = theBoard.HashLock();
			Table[key].theEval = eval;
			Table[key].theDepth = depth;
			Table[key].theEvalType = evalType;
			Table[key].timeStamp = timeStamp;
			return true;
		}
	}
}
