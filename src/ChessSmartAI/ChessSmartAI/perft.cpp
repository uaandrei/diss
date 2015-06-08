#include <stdio.h>
#include "defs.h"

int leafNodes = 0;

void Perft(int depth, S_BOARD *pos) {
	ASSERT(CheckBoard(pos));

	if (depth == 0) {
		leafNodes++;
		return;
	}

	S_MOVELIST list[1];
	GenerateAllMoves(pos, list);

	int moveNum = 0;
	for (moveNum = 0; moveNum < list->count; ++moveNum) {
		if (!MakeMove(pos, list->moves[moveNum].move)) {
			continue;
		}
		Perft(depth - 1, pos);
		TakeMove(pos);
	}

	return;
}

void PerftTest(int depth, S_BOARD *pos) {
	ASSERT(CheckBoard(pos));

	PrintBoard(pos);
	printf("\nStarting Test To Depth:%d\n", depth);
	leafNodes = 0;
	int start = GetTimeMs();

	S_MOVELIST list[1];
	GenerateAllMoves(pos, list);

	int move = 0;
	int moveNum = 0;
	for (moveNum = 0; moveNum < list->count; ++moveNum) {
		move = list->moves[moveNum].move;
		if (!MakeMove(pos, move)) {
			continue;
		}
		long cumNodes = leafNodes;
		Perft(depth - 1, pos);
		TakeMove(pos);
		long oldNodes = leafNodes - cumNodes;
		printf("move %d : %s : %d\n", moveNum + 1, PrMove(move), oldNodes);
	}

	printf("\nTest Complete : %ld nodes visited in %dms\n", leafNodes, GetTimeMs() - start);

	return;
}