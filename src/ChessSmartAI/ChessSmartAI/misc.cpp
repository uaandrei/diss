#include <stdio.h>
#include "defs.h"

//#if defined _WIN32
#include <Windows.h>
//#elif defined _WIN64
//#include <Windows.h>
//#else
//#include <time.h>
//#endif

int GetTimeMs() {
	return GetTickCount();
}