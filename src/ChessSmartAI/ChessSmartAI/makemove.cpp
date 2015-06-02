#include <stdio.h>
#include "defs.h"

#define HASH_PCE(pce, sq) (pos->posKey ^= (PieceKeys[( pce )][( sq )]))
#define HASH_CA (pos->posKey ^= (CastleKeys[( pos->castlePerm )]))
#define HASH_SIDE (pos->posKey ^= (SideKey))
#define HASH_EP (pos->posKey ^= (PieceKeys[EMPTY][(pos->enPas)]))

// castlePerm &= CastlePerm[from]
// 1111 == 15 - KQkq
// wK move => castlePerm & 0011 (3) => kq (no white castling) etc.

const int CastlePerm[120] = {
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 13, 15, 15, 15, 12, 15, 15, 14, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 7, 15, 15, 15, 3, 15, 15, 11, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
	15, 15, 15, 15, 15, 15, 15, 15, 15, 15
};

static void ClearPiece(const int sq, S_BOARD *pos) {
	ASSERT(SqOnBoard(sq));

	int pce = pos->pieces[sq];

	ASSERT(PieceValid(pce));

	int col = PieceCol[pce];
	int index = 0;
	int t_numPce = -1;

	HASH_PCE(pce, sq);

	pos->pieces[sq] = EMPTY;
	pos->material[col] -= PieceVal[pce];

	if (PieceBig[pce]) {
		pos->bigPce[col]--;
		if (PieceMaj[pce]) {
			pos->majPce[col]--;
		}
		else{
			pos->minPce[col]--;
		}
	}
	else {
		CLRBIT(pos->pawns[col], SQ64(sq));
		CLRBIT(pos->pawns[BOTH], SQ64(sq));
	}

	/*  e.g.
		pos->pceNum[wP] == 5, looping from 0 to 4
		pos->pList[pce][0] = sq0
		pos->pList[pce][1] = sq1 <-- remove
		pos->pList[pce][2] = sq2
		pos->pList[pce][3] = sq3
		pos->pList[pce][4] = sq4

		sq == sq1 so t_numPce = 1
		*/
	for (index = 0; index < pos->pceNum[pce]; ++index) {
		if (pos->pList[pce][index] == sq) {
			t_numPce = index;
			break;
		}
	}

	ASSERT(t_numPce != -1);

	pos->pceNum[pce]--;
	// pos->pceNum[wP] == 4

	pos->pList[pce][t_numPce] = pos->pList[pce][pos->pceNum[pce]];
	// pos->pList[wP][1] = pos->pList[wP][4] = sq4

	/*
		pos->pList[pce][0] = sq0
		pos->pList[pce][1] = sq4
		pos->pList[pce][2] = sq2
		pos->pList[pce][3] = sq3
		*/
}

static void AddPiece(const int sq, S_BOARD *pos, const int pce) {
	ASSERT(PieceValid(pce));
	ASSERT(SqOnBoard(sq));

	int col = PieceCol[pce];
	HASH_PCE(pce, sq);
	pos->pieces[sq] = pce;

	if (PieceBig[pce]) {
		pos->bigPce[col]++;
		if (PieceMaj[pce]) {
			pos->majPce[col]++;
		}
		else{
			pos->minPce[col]++;
		}
	}
	else {
		SETBIT(pos->pawns[col], SQ64(sq));
		SETBIT(pos->pawns[BOTH], SQ64(sq));
	}
	pos->material[col] += PieceVal[pce];
	pos->pList[pce][pos->pceNum[pce]++] = sq;
}

static void MovePiece(const int from, const int to, S_BOARD *pos) {
	ASSERT(SqOnBoard(from));
	ASSERT(SqOnBoard(to));

	int index = 0;
	int pce = pos->pieces[from];
	int col = PieceCol[pce];
#ifdef DEBUG
	int t_PieceNum = FALSE;
#endif

	HASH_PCE(pce, from);
	pos->pieces[from] = EMPTY;

	HASH_PCE(pce, to);
	pos->pieces[to] = pce;

	if (!PieceBig[pce]) {
		CLRBIT(pos->pawns[col], SQ64(from));
		CLRBIT(pos->pawns[BOTH], SQ64(from));
		SETBIT(pos->pawns[col], SQ64(to));
		SETBIT(pos->pawns[BOTH], SQ64(to));
	}

	for (index = 0; index < pos->pceNum[pce]; ++index) {
		if (pos->pList[pce][index] == from) {
			pos->pList[pce][index] = to;
#ifdef DEBUG
			t_PieceNum = TRUE;
#endif
			break;
		}
	}

	ASSERT(t_PieceNum);
}

void MakeMove(S_BOARD *pos) {

}