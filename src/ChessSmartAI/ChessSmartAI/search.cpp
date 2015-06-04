#include <stdio.h>
#include "defs.h"

static void CheckUp() {
	// check if time up, or interrupt from GUI
}

static int IsRepetition(const S_BOARD *pos) {
	int index = 0;

	// fifty moves rule is reset to 0 when capture is made or pawn is moved,
	// when fifty moves is reset that means that it's a position that can't be repeated
	// (pawns can't go backward and pieces cannot be uncaptured)
	// hisPly - fiftyMove = number of positions that can be repeated
	for (index = pos->hisPly - pos->fiftyMove; index < pos->hisPly - 1; ++index) {

		ASSERT(index >= 0 && index <= MAXGAMEMOVES);

		if (pos->posKey == pos->history[index].posKey) {
			return TRUE;
		}
	}
	return FALSE;
}

// prepare for new search, clear everything
static void ClearForSearch(S_BOARD *pos, S_SEARCHINFO *info) {

}

/*
horizont effect:
	-let's say that at the botton of the tree the last move was made by W and captured a bN with wQ
	-this might seem like a good move, but what if black could've replied in this tree and taken the wQ
	-then W would've taken a bN but lost a wQ, W actually lost
solution quiescence search: you carry on with AB but you dont have a depth and only generated capture moves
	-you basically search all captures until they've been resolved and only then do you return the evaluation of the position
	-it's an attempt to find a quiet position from which we can get an evaluation so we can eliminate the horizon effect
*/
static int QuiescenceSearch(int alpha, int beta, S_BOARD *pos, S_SEARCHINFO *info) {
	return 0;
}

static int AlphaBeta(int alpha, int beta, int depth, S_BOARD *pos, S_SEARCHINFO *info, int doNull) {
	return 0;
}

void SearchPositions(S_BOARD *pos, S_SEARCHINFO *info) {
	// ... interative deepening, search initialization
}