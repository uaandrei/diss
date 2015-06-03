#include <stdio.h>
#include "defs.h"

char *PrSq(const int sq) {
	// static so it exists once the function exits
	static char SqStr[3];

	int file = FilesBrd[sq];
	int rank = RanksBrd[sq];

	sprintf_s(SqStr, sizeof(SqStr), "%c%c", ('a' + file), ('1' + rank));

	return SqStr;
}

char *PrMove(const int move) {
	static char MvStr[6];

	int ff = FilesBrd[FROMSQ(move)];
	int rf = RanksBrd[FROMSQ(move)];
	int ft = FilesBrd[TOSQ(move)];
	int rt = RanksBrd[TOSQ(move)];

	int promoted = PROMOTED(move);

	if (promoted) {
		char pchar = 'q';
		if (IsKn(promoted)) {
			pchar = 'n';
		} else if (IsRQ(promoted) && !IsBQ(promoted)) {
			pchar = 'r';
		} else if (!IsRQ(promoted) && IsBQ(promoted)) {
			pchar = 'b';
		}
		sprintf_s(MvStr, sizeof(MvStr), "%c%c%c%c%c", ('a' + ff), ('1' + rf), ('a' + ft), ('1' + rt), pchar);
	} else {
		sprintf_s(MvStr, sizeof(MvStr), "%c%c%c%c", ('a' + ff), ('1' + rf), ('a' + ft), ('1' + rt));
	}

	return MvStr;
}

// g7g8q e1g1
int ParseMove(char *ptrChar, S_BOARD *pos) {
	if (ptrChar[1] > '8' || ptrChar[1] < '1') return NOMOVE;
	if (ptrChar[3] > '8' || ptrChar[3] < '1') return NOMOVE;
	if (ptrChar[0] > 'h' || ptrChar[0] < 'a') return NOMOVE;
	if (ptrChar[2] > 'h' || ptrChar[2] < 'a') return NOMOVE;

	int from = FR2SQ(ptrChar[0] - 'a', ptrChar[1] - '1');
	int to = FR2SQ(ptrChar[2] - 'a', ptrChar[3] - '1');

	ASSERT(SqOnBoard(from) && SqOnBoard(to));

	S_MOVELIST list[1];
	GenerateAllMoves(pos, list);

	int moveNum = 0;
	int move = 0;
	int promPce = EMPTY;

	for (moveNum = 0; moveNum < list->count; ++moveNum) {
		move = list->moves[moveNum].move;
		if (FROMSQ(move) == from && TOSQ(move) == to) {
			promPce = PROMOTED(move);
			if (promPce != EMPTY) {
				if (IsRQ(promPce) && !IsBQ(promPce) && ptrChar[4] == 'r') {
					return move;
				} else if (!IsRQ(promPce) && IsBQ(promPce) && ptrChar[4] == 'b') {
					return move;
				} else if (IsRQ(promPce) && IsBQ(promPce) && ptrChar[4] == 'q') {
					return move;
				} else if (IsKn(promPce) && ptrChar[4] == 'n') {
					return move;
				}
				continue;
			}
			return move;
		}
	}
	return NOMOVE;
}

void PrintMoveList(const S_MOVELIST *list) {
	int index = 0;
	int score = 0;
	int move = 0;
	printf("MoveList:%d\n", list->count);

	for (index = 0; index < list->count; ++index) {
		move = list->moves[index].move;
		score = list->moves[index].score;

		printf("Move:%d > %s (score:%d)\n", index + 1, PrMove(move), score);
	}
	printf("MoveList Total %d Moves\n\n", list->count);
}