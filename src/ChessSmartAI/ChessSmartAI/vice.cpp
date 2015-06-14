#include <stdio.h>
#include <stdlib.h>
#include "defs.h"

#define LEGENDARYFEN "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1"
#define FEN1 "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1"
//#define FEN1 "8/3q4/8/8/4Q3/8/8/8 w - - 0 2 "
//#define FEN1 "8/3q1p2/8/5P2/4Q3/8/8/8 w - - 0 2 "
#define FEN2 "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2"
#define FEN3 "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2"
#define FEN4 "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1"
#define FENPAWNMOVESW "rnbqkb1r/pp1p1pPp/8/2p1pP2/1P1P4/3P3P/P1P1P3/RNBQKBNR w KQkq e6 0 1"
#define FENPAWNMOVESB "rnbqkbnr/p1p1p3/3p3p/1p1p4/2P1Pp2/8/PP1P1PpP/RNBQKB1R b - e3 0 1"
#define KNIGHTSKINGSFEN "5k2/1n6/4n3/6N1/8/3N4/8/5K2 w - - 0 1"
#define ROOKSFEN "6k1/8/5r2/8/1nR5/5N2/8/6K1 b - - 0 1"
#define QUEENSFEN "6k1/8/4nq2/8/1nQ5/5N2/1N6/6K1 b - - 0 1"
#define BISHOPSFEN "6k1/1b6/4n3/8/1n4B1/1B3N2/1N6/2b3K1 b - - 0 1"
#define CASTLE1FEN "r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 1"
#define CASTLE2FEN "3rk2r/8/8/8/8/8/6p1/R3K2R b KQk - 0 1"
#define CASTLE3FEN "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 1"
#define PERFTFEN "n1n5/PPPk4/8/8/8/8/4Kppp/5N1N w - - 0 1"
#define WAC2 "2rr3k/pp3pp1/1nnqbN1p/3pN3/2pP4/2P3Q1/PPB4P/R4RK1 w - -" // mate in 3
#define WAC1 "r1b1k2r/ppppnppp/2n2q2/2b5/3NP3/2P1B3/PP3PPP/RN1QKB1R w KQkq - 0 1"

void PrintBin(int move) {
	int index = 0;
	printf("\nbinary:");
	for (index = 27; index >= 0; index--) {
		if ((1 << index) & move) printf("1");
		else printf("0");
		if (index != 28 && index % 4 == 0) printf(" ");
	}
	printf("\n");
}

void print64board120board() {
	int index = 0;
	for (index = 0; index < BRD_SQ_NUM; ++index) {
		if (index % 10 == 0) printf("\n");
		printf("%5d", Sq120ToSq64[index]);
	}

	printf("\n");
	printf("\n");

	for (index = 0; index < 64; ++index) {
		if (index % 8 == 0) printf("\n");
		printf("%5d", Sq64ToSq120[index]);
	}
}

void ShowSqAttackedBySide(const int side, const S_BOARD *pos) {
	int rank = 0, file = 0, sq = 0;
	printf("\n\nSquare attacked by:%c\n", SideChar[side]);
	for (rank = RANK_8; rank >= RANK_1; --rank) {
		for (file = FILE_A; file <= FILE_H; ++file) {
			sq = FR2SQ(file, rank);
			if (SqAttacked(sq, side, pos)) {
				printf("X");
			} else {
				printf("-");
			}
		}
		printf("\n");
	}
	printf("\n\n");
}

//Functions that print
//PrintBitBoard
//PrintBoard
//PrintMoveList
//PerftTest

// return format:
//		non promotion: a2a3;12311
//		    promotion: a2a3q;12311
extern "C" __declspec(dllexport) char* __cdecl GetMove(char *fen, int depth) {
	static char result[50];
	S_BOARD board[1];
	S_SEARCHINFO info[1];

	AllInit();

	InitPvTable(board->PvTable);
	ParseFen(fen, board);

	info->depth = depth;
	info->timeSet = FALSE;

	SearchPositions(board, info);

	sprintf_s(result, "%s;%d", PrMove(info->bestMove), info->bestScore);

	FreePvTable(board->PvTable);
	return result;
}

void ConsoleLoop() {

	AllInit();

	S_BOARD board[1];
	InitPvTable(board->PvTable);

	S_SEARCHINFO info[1];



	ParseFen(WAC2, board);
	//PerftTest(3, board);

	char input[6];
	int move = NOMOVE;
	int pvNum = 0;
	int max = 0;

	while (TRUE) {
		PrintBoard(board);
		printf("Please enter a move: ");
		fgets(input, 5, stdin);

		if (input[0] == 'q') {
			break;
		} else if (input[0] == 't') {
			TakeMove(board);
		} else if (input[0] == 's') {
			info->depth = 6;
			info->timeSet = FALSE;
			info->startTime = GetTimeMs();
			info->stopTime = GetTimeMs() + 1000;
			SearchPositions(board, info);

			printf("best move:%s best score:%d time:%dms\n", PrMove(info->bestMove), info->bestScore, GetTimeMs() - info->startTime);
			if (info->bestScore == -MATE) {
				printf("CHECK MATE. side %s lost!", board->side == WHITE ? "WHITE" : "BLACK");
				break;
			}
		} else {
			move = ParseMove(input, board);
			if (move != NOMOVE) {
				StorePvMove(board, move);
				MakeMove(board, move);
			} else {
				printf("Move not parsed:%s\n", input);
			}
		}

		fflush(stdin);
	}

	getchar();

	FreePvTable(board->PvTable);
}

int main(){
//	char *result = GetMove(WAC2, 6);
	ConsoleLoop();
	return 0;
}