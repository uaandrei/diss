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

int main() {

	AllInit();

	S_BOARD board[1];

	ParseFen(START_FEN, board);
	//PerftTest(3, board);

	char input[6];
	int move = NOMOVE;
	int pvNum = 0;
	int max = 0;

	while (TRUE) {
		PrintBoard(board);
		printf("Please enter a move: ");
		fgets(input, 6, stdin);

		if (input[0] == 'q') {
			break;
		} else if (input[0] == 't') {
			TakeMove(board);
		} else if (input[0] == 'p') {
			//PerftTest(4, board);
			max = GetPvLine(4, board);
			printf("PvLine of %d moves: ", max);
			for (pvNum = 0; pvNum < max; ++pvNum) {
				move = board->PvArray[pvNum];
				printf(" %s", PrMove(move));
			}
			printf("\n");
		} else {
			move = ParseMove(input, board);
			if (move != NOMOVE) {
				StorePvMove(board, move);
				MakeMove(board, move);
				/*if (IsRepetition(board)) {
					printf("REPETITION SEEN\n");
					}*/
			} else {
				printf("Move not parsed:%s\n", input);
			}
		}

		fflush(stdin);
	}

	getchar();
	return 0;
}