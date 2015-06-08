#include <stdio.h>
#include <stdlib.h>
#include "defs.h"

// 0000 000000000000000000 000000000000000000 000000000000000000 111111111111111111
// 0000 000000000000000000 000000000000000000 111111111111111111 000000000000000000
// 0000 000000000000000000 111111111111111111 000000000000000000 000000000000000000
// 0000 000000000000000000 000000000000000000 000000000000000000 000000000000000000
// 1111 111111111111111111 000000000000000000 000000000000000000 000000000000000000
// rand() generated 15 bit random number
// for filling all 15 bit groups and our TRUE random 64 bit number, we shift by 15 30 45
// 0xf = 1111, we shift by 60 to fill last 4 bits
// or all numbers => random 64 bit number
#define RAND_64 ( (U64)rand() | \
				( (U64)rand() << 15 ) | \
				( (U64)rand() << 30 ) | \
				( (U64)rand() << 45 ) | \
				( ((U64)rand() & 0xf) << 60 ) )

int Sq120ToSq64[BRD_SQ_NUM];
int Sq64ToSq120[64];

U64 SetMask[64];
U64 ClearMask[64];

// including for empty, hence 13 (used for hashing in en passant)
U64 PieceKeys[13][120];

// hashkey for white side
U64 SideKey;

// 0000 - 1111 <=> 0 - 15 castle keys
U64 CastleKeys[16];

int FilesBrd[BRD_SQ_NUM];
int RanksBrd[BRD_SQ_NUM];

void InitFilesRanksBrd() {
	int index = 0;
	int file = FILE_A;
	int rank = RANK_1;
	int sq = A1;
	int sq64 = 0;

	for (index = 0; index < BRD_SQ_NUM; ++index) {
		FilesBrd[index] = OFFBOARD;
		RanksBrd[index] = OFFBOARD;
	}
	for (rank = RANK_1; rank <= RANK_8; ++rank) {
		for (file = FILE_A; file <= FILE_H; ++file) {
			sq = FR2SQ(file, rank);
			FilesBrd[sq] = file;
			RanksBrd[sq] = rank;
		}
	}
}

void InitHashKeys() {
	int index = 0;
	int index2 = 0;
	for (index = 0; index < 13; ++index) {
		for (index2 = 0; index2 < 120; ++index2) {
			PieceKeys[index][index2] = RAND_64;
		}
	}
	SideKey = RAND_64;
	for (index = 0; index < 16; ++index) {
		CastleKeys[index] = RAND_64;
	}
}

void InitSq120To64() {
	int index = 0;
	int file = FILE_A;
	int rank = RANK_1;
	int sq = A1;
	int sq64 = 0;

	for (index = 0; index < BRD_SQ_NUM; ++index) {
		// 65 should never be returned, failsafe
		Sq120ToSq64[index] = 65;
	}
	for (index = 0; index < 64; ++index) {
		// 120 should never be returned, failsafe
		Sq64ToSq120[index] = 120;
	}
	for (rank = RANK_1; rank <= RANK_8; ++rank) {
		for (file = FILE_A; file <= FILE_H; ++file) {
			sq = FR2SQ(file, rank);
			Sq64ToSq120[sq64] = sq;
			Sq120ToSq64[sq] = sq64;
			sq64++;
		}
	}
}

void InitBitMasks() {
	int index = 0;

	for (index = 0; index < 64; index++) {
		SetMask[index] = 0ULL;
		ClearMask[index] = 0ULL;
	}
	for (index = 0; index < 64; index++) {
		SetMask[index] |= (1ULL << index);
		ClearMask[index] = ~SetMask[index];
	}
}

void AllInit() {
	InitSq120To64();
	InitBitMasks();
	InitHashKeys();
	InitFilesRanksBrd();
	InitMvvLva();
}