#include <stdio.h>
#include "defs.h"

#define INFINITE 30000

static void CheckUp(S_SEARCHINFO *info) {
	if (info->timeSet == TRUE && GetTimeMs() > info->stopTime) {
		info->stopped = TRUE;
	}
}

// moveNum - where we are in our move loop
// we swap best scoring move with the move @moveNum => we make the best move
static void PickNextMove(int moveNum, S_MOVELIST *list) {
	S_MOVE temp;
	int index = 0;
	int bestScore = 0;
	int bestNum = moveNum;

	for (index = moveNum; index < list->count; ++index) {
		if (list->moves[index].score > bestScore) {
			bestScore = list->moves[index].score;
			bestNum = index;
		}
	}

	temp = list->moves[moveNum];
	list->moves[moveNum] = list->moves[bestNum];
	list->moves[bestNum] = temp;
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
	int index = 0, index2 = 0;

	for (index = 0; index < 13; ++index) {
		for (index2 = 0; index2 < BRD_SQ_NUM; ++index2) {
			pos->searchHistory[index][index2] = 0;
		}
	}

	for (index = 0; index < 2; ++index) {
		for (index2 = 0; index2 < MAXDEPTH; ++index2) {
			pos->searchKillers[index][index2] = 0;
		}
	}

	ClearPvTable(pos->PvTable);
	pos->ply = 0;

	info->stopped = 0;
	info->nodes = 0;
	info->fh = 0;
	info->fhf = 0;
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
	ASSERT(CheckBoard(pos));

	// check that runs every 2048 nodes
	if ((info->nodes & 2047) == 0) {
		CheckUp(info);
	}

	info->nodes++;
	if (IsRepetition(pos) || pos->fiftyMove >= 100) {
		return 0;
	}
	if (pos->ply > MAXDEPTH - 1) {
		return EvalPosition(pos);
	}

	// see how you're doing if you don't even make a move 
	// if you're already doing better than beta then you're doing pretty well
	int score = EvalPosition(pos);
	if (score >= beta) {
		return beta;
	}

	if (score > alpha) {
		alpha = score;
	}

	S_MOVELIST list[1];
	GenerateAllCaptures(pos, list);

	int moveNum = 0;
	int legal = 0;
	int oldAlpha = alpha;
	int bestMove = NOMOVE;
	score = -INFINITE;
	int pvMove = ProbePvTable(pos);

	for (moveNum = 0; moveNum < list->count; ++moveNum) {
		PickNextMove(moveNum, list);
		if (!MakeMove(pos, list->moves[moveNum].move)) {
			continue;
		}

		legal++;
		score = -QuiescenceSearch(-beta, -alpha, pos, info);
		TakeMove(pos);

		if (info->stopped == TRUE) {
			return 0;
		}

		if (score > alpha) {
			if (score >= beta) {
				if (legal == 1) {
					info->fhf++;
				}
				info->fh++;
				return beta;
			}
			alpha = score;
			bestMove = list->moves[moveNum].move;
		}
	}

	if (alpha != oldAlpha) {
		StorePvMove(pos, bestMove);
	}

	return alpha;
}

static int AlphaBeta(int alpha, int beta, int depth, S_BOARD *pos, S_SEARCHINFO *info, int doNull) {
	ASSERT(CheckBoard(pos));

	if (depth == 0) {
		return QuiescenceSearch(alpha, beta, pos, info);
		//return EvalPosition(pos);
	}

	// check that runs every 2048 nodes
	if ((info->nodes & 2047) == 0) {
		CheckUp(info);
	}

	info->nodes++;

	if (IsRepetition(pos) || pos->fiftyMove >= 100) {
		return 0;
	}
	if (pos->ply > MAXDEPTH - 1) {
		return EvalPosition(pos);
	}

	S_MOVELIST list[1];
	GenerateAllMoves(pos, list);

	int moveNum = 0;
	int legal = 0;
	int oldAlpha = alpha;
	int bestMove = NOMOVE;
	int score = -INFINITE;
	int pvMove = ProbePvTable(pos);

	if (pvMove != NOMOVE) {
		for (moveNum = 0; moveNum < list->count; ++moveNum) {
			if (list->moves[moveNum].move == pvMove) {
				// 2000000 so we search it first
				list->moves[moveNum].score = 2000000;
				break;
			}
		}
	}

	for (moveNum = 0; moveNum < list->count; ++moveNum) {
		PickNextMove(moveNum, list);
		if (!MakeMove(pos, list->moves[moveNum].move)) {
			continue;
		}

		legal++;
		score = -AlphaBeta(-beta, -alpha, depth - 1, pos, info, TRUE);
		TakeMove(pos);

		if (info->stopped == TRUE) {
			return 0;
		}

		if (score > alpha) {
			if (score >= beta) {
				if (legal == 1) {
					info->fhf++;
				}
				info->fh++;

				// if not a capture
				// adds killers in for non-captures that are causing beta cut-offs to the search 
				if (!(list->moves[moveNum].move & MFLAGCAP)) {
					pos->searchKillers[1][pos->ply] = pos->searchKillers[0][pos->ply];
					pos->searchKillers[0][pos->ply] = list->moves[moveNum].move;
				}

				return beta;
			}
			alpha = score;
			bestMove = list->moves[moveNum].move;

			if (!(list->moves[moveNum].move & MFLAGCAP)) {
				// it prioritises moves that ocurred near the root of the tree
				pos->searchHistory[pos->pieces[FROMSQ(bestMove)]][TOSQ(bestMove)] += depth;
			}
		}
	}

	if (legal == 0) {
		if (SqAttacked(pos->KingSq[pos->side], pos->side ^ 1, pos)) {
			return -MATE + pos->ply;
		} else {
			return 0;
		}
	}
	if (alpha != oldAlpha) {
		StorePvMove(pos, bestMove);
	}

	return alpha;
}

void SearchPositions(S_BOARD *pos, S_SEARCHINFO *info) {
	// for depth = 1 to maxDepth
	//		search with AB
	//		next depth

	int bestMove = NOMOVE;
	int bestScore = -INFINITE;
	int currentDepth = 0;
	int pvMoves = 0;
	int pvNum = 0;
	ClearForSearch(pos, info);

	// ... interative deepening, search initialization
	for (currentDepth = 1; currentDepth <= info->depth; ++currentDepth) {
		bestScore = AlphaBeta(-INFINITE, INFINITE, currentDepth, pos, info, TRUE);

		if (info->stopped == TRUE) {
			break;
		}

		// number of pv moves
		pvMoves = GetPvLine(currentDepth, pos);

		// best one is at the top of pv moves array
		bestMove = pos->PvArray[0];

		//pvMoves = GetPvLine(currentDepth, pos);
#ifdef DEBUG
		printf("Pv");
		for (pvNum = 0; pvNum < pvMoves; ++pvNum) {
			printf(" %s", PrMove(pos->PvArray[pvNum]));
		}
		printf("\n");
#endif
	}
	info->bestMove = bestMove;
	info->bestScore = bestScore;
}