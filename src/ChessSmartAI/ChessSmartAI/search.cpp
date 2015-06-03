#include <stdio.h>
#include "defs.h"

int IsRepetition(const S_BOARD *pos) {
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

void SearchPositions(S_BOARD *pos) {

}