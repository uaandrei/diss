#include <stdio.h>
#include "defs.h"

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

int main() {
	AllInit();

	U64 bb = 0ULL;
	int index = 0;

	getchar();
	return 0;
}

