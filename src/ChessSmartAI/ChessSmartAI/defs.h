#ifndef DEFS_H
#define DEFS_H

#include <stdlib.h>

//#define DEBUG
#ifndef DEBUG
#define ASSERT(n)
#else
#define ASSERT(n) \
	if(!(n)) { \
		printf("%s - Failed", #n); \
		printf("On %s ", __DATE__); \
		printf("At %s ", __TIME__); \
		printf("In File %s ", __FILE__); \
		printf("At Line %d\n ", __LINE__); \
		getchar(); \
		exit(1); }
#endif

// bitboard
// least significant bit = A1 (0) all the way to H8 (63)
typedef unsigned long long U64;

#define NAME "Vice 1.0"
#define BRD_SQ_NUM 120

#define MAXGAMEMOVES 2048 // halfmoves
#define MAXPOSITIONMOVES 256
#define MAXDEPTH 64

#define MATE 29000

#define START_FEN "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"

// board data
enum { EMPTY, wP, wN, wB, wR, wQ, wK, bP, bN, bB, bR, bQ, bK };
enum { FILE_A, FILE_B, FILE_C, FILE_D, FILE_E, FILE_F, FILE_G, FILE_H, FILE_NONE };
enum { RANK_1, RANK_2, RANK_3, RANK_4, RANK_5, RANK_6, RANK_7, RANK_8, RANK_NONE };
enum { WHITE, BLACK, BOTH };
enum {
	A1 = 21, B1, C1, D1, E1, F1, G1, H1,
	A2 = 31, B2, C2, D2, E2, F2, G2, H2,
	A3 = 41, B3, C3, D3, E3, F3, G3, H3,
	A4 = 51, B4, C4, D4, E4, F4, G4, H4,
	A5 = 61, B5, C5, D5, E5, F5, G5, H5,
	A6 = 71, B6, C6, D6, E6, F6, G6, H6,
	A7 = 81, B7, C7, D7, E7, F7, G7, H7,
	A8 = 91, B8, C8, D8, E8, F8, G8, H8, NO_SQ, OFFBOARD
};
enum { FALSE, TRUE };

// 4 bits for castling: 0001 0010 0100 1000
enum { WKCA = 1, WQCA = 2, BKCA = 4, BQCA = 8 };

typedef struct {

	// all the information for the move
	int move;

	// used for move ordering
	int score;
} S_MOVE;

typedef struct {
	S_MOVE moves[MAXPOSITIONMOVES];
	int count;
} S_MOVELIST;

typedef struct {
	U64 posKey;
	int move;
} S_PVENTRY;

typedef struct {
	S_PVENTRY *pTable;
	int numEntries;
} S_PVTABLE;

typedef struct {

	int move;
	int castlePerm;
	int enPas;
	int fiftyMove;
	U64 posKey;

} S_UNDO;

// board structure
typedef struct {

	// representation for all board squares (120: 0 - 119)
	int pieces[BRD_SQ_NUM];

	// bitboards for pawns (White, Black, BOTH)
	U64 pawns[3];

	int KingSq[2];

	// side to move
	int side;

	// En Passant
	int enPas;

	// where castling is permitted
	int castlePerm;

	int fiftyMove;

	// ply == half move; (2 x ply = 1 move)
	// half moves played in the current search
	int ply;

	// history half moves played in the whole game
	int hisPly;

	// unique key for each position
	U64 posKey;

	// number of pieces on board by type
	int pceNum[13];

	// number of big (not pawn) pieces by color(White, Black)
	int bigPce[2];

	// number of major (r, q & k) pieces by color(White, Black)
	int majPce[2];

	// number of minor (b & n) pieces by color(White, Black)
	int minPce[2];

	// material score by color(White, Black)
	int material[2];

	S_UNDO history[MAXGAMEMOVES];

	// piece list
	int pList[13][10];
	// must be maintained, but is worth it. rather then looping through all the pieces
	// pList[wK][0] = E1
	// pList[wK][1] = D4 ....

	S_PVTABLE PvTable[1];
	int PvArray[MAXDEPTH];

	// every time a move beats alpha: searchHistory[pieceType, toSquare]++   || non-capture
	int searchHistory[13][BRD_SQ_NUM];

	// stores two arrays of moves indexed by depth that recently caused a beta cut-off  || non-capture
	// 0 best slot, 1 old slot
	int searchKillers[2][MAXDEPTH];

} S_BOARD;

typedef struct {
	int startTime;
	int stopTime;
	int depth;
	int depthSet;
	int timeSet;
	int movesToGo;
	int infinite;

	int bestMove;
	int bestScore;

	// count of all the posistions the engine visits in a search tree
	long nodes;

	int quit;
	int stopped;

	// fail high       - counts the number of times it searches the next legal moves after the first one
	// fail high first - counts the number of times it searches the first legal move
	// fhf/fh - is actually an indicator of how well the beta cutoffs are working. ideal = 1
	float fh;
	float fhf;

} S_SEARCHINFO;

// GAME MOVE
// hexadecimals are easy to represent on 4 bits:
// 1000 1010 1111 -> 8FF (hexa, prefix : 0x)
/*
*7 bits are enough to represent a move 7 bits = 127, square values >= 21 <= 98
*if pawn moved two squares
0000 0000 0000 0000 0000 0111 1111 -> From                 0x7F
0000 0000 0000 0011 1111 1000 0000 -> To             >> 7  0x7F
0000 0000 0011 1100 0000 0000 0000 -> Captured piece >> 14 0xF
0000 0000 0100 0000 0000 0000 0000 -> EP Capture           0x40000
0000 0000 1000 0000 0000 0000 0000 -> PawnStart            0x80000
0000 1111 0000 0000 0000 0000 0000 -> Promoted to	 >> 20 0xF
0001 0000 0000 0000 0000 0000 0000 -> Castling move	       0x1000000
*/

#define FROMSQ(m) (( m ) & 0x7F)
#define TOSQ(m) (( ( m ) >> 7 ) & 0x7F)
#define CAPTURED(m) (( ( m ) >> 14 ) & 0xF)
#define PROMOTED(m) (( ( m ) >> 20 ) & 0xF)

#define MFLAGEP 0x40000 // en passant capture move
#define MFLAGPS 0x80000 // two square start pawn move
#define MFLAGCA 0x1000000 // castling move

#define MFLAGCAP 0x7C000 // EP + Captured piece BITS
#define MFLAGPROM 0xF00000

#define NOMOVE 0

// MACROS
#define FR2SQ(f, r) ( ( 21 + (f)) + ( (r) * 10 ) ) // 120 based
#define SQ64(sq120) ( Sq120ToSq64[( sq120 )] )
#define SQ120(sq64) ( Sq64ToSq120[( sq64 )] )
#define POP(b) PopBit(b)
#define CNT(b) CountBits(b)
#define CLRBIT(bb,sq) ((bb) &= ClearMask[(sq)])
#define SETBIT(bb,sq) ((bb) |= SetMask[(sq)])

#define IsBQ(p) ( PieceBishopQueen[( p )] )
#define IsRQ(p) ( PieceRookQueen[( p )] )
#define IsKn(p) ( PieceKnight[( p )] )
#define IsKi(p) ( PieceKing[( p )] )

// GLOBALS
extern int Sq120ToSq64[BRD_SQ_NUM];
extern int Sq64ToSq120[64];
extern U64 SetMask[64];
extern U64 ClearMask[64];
extern U64 PieceKeys[13][120];
extern U64 SideKey;
extern U64 CastleKeys[16];
extern char PceChar[];
extern char SideChar[];
extern char RankChar[];
extern char FileChar[];

extern int PieceBig[13];
extern int PieceMaj[13];
extern int PieceMin[13];
extern int PieceVal[13];
extern int PieceCol[13];
extern int PiecePawn[13];

extern int FilesBrd[BRD_SQ_NUM];
extern int RanksBrd[BRD_SQ_NUM];

extern int PieceKnight[13];
extern int PieceKing[13];
extern int PieceRookQueen[13];
extern int PieceBishopQueen[13];
extern int PieceSlides[13];

// FUNCTIONS

// init.cpp
extern void AllInit();

// bitboards.cpp
extern void PrintBitBoard(U64 bb);
extern int CountBits(U64 bb);
extern int PopBit(U64 *bb);

// hashkeys.cpp
extern U64 GeneratePosKey(const S_BOARD *pos);

// board.cpp
extern void ResetBoard(S_BOARD *pos);
extern int ParseFen(char *fen, S_BOARD *pos);
extern void PrintBoard(const S_BOARD *pos);
extern void UpdateListMaterial(S_BOARD *pos);
extern int CheckBoard(const S_BOARD *pos);

// attack.cpp
extern int SqAttacked(const int sq, const int side, const S_BOARD *pos);

// io.cpp
extern char *PrSq(const int sq);
extern char *PrMove(const int move);
extern void PrintMoveList(const S_MOVELIST *list);
extern int ParseMove(char *ptrChar, S_BOARD *pos);

// validate.cpp
extern int SqOnBoard(const int sq);
extern int SideValid(const int side);
extern int FileRankValid(const int fr);
extern int PieceValidEmpty(const int pce);
extern int PieceValid(const int pce);

// movegen.cpp
extern void GenerateAllMoves(const S_BOARD *pos, S_MOVELIST *list);
extern void GenerateAllCaptures(const S_BOARD *pos, S_MOVELIST *list);
extern int MoveExists(S_BOARD *pos, const int move);
extern void InitMvvLva();

// makemove.cpp
extern int MakeMove(S_BOARD *pos, int move);
extern void TakeMove(S_BOARD *pos);

// perft.cpp
extern void PerftTest(int depth, S_BOARD *pos);

// search.cpp
extern void SearchPositions(S_BOARD *pos, S_SEARCHINFO *info);

// misc.cpp
extern int GetTimeMs();

// pvtable.cpp
extern void InitPvTable(S_PVTABLE *table);
extern void FreePvTable(S_PVTABLE *table);
extern void StorePvMove(const S_BOARD *pos, const int move);
extern int ProbePvTable(const S_BOARD *pos);
extern int GetPvLine(const int depth, S_BOARD *pos);
extern void ClearPvTable(S_PVTABLE *table);

// evaluate.cpp
extern int EvalPosition(const S_BOARD *pos);

#endif