using System.Collections;
using System.Collections.Generic;
namespace Chess.SmartAI
{
    public class jcHistoryTable
    {
        /***********************************************************************
         * DATA MEMBERS
         **********************************************************************/

        // the table itself; a separate set of cutoff counters exists for each
        // side
        static int[][][] History;
        static int[][] CurrentHistory;

        // This is a singleton class; the same history can be shared by two AI's
        private static jcHistoryTable theInstance;

        // A comparator, used to sort the moves
        jcMoveComparator MoveComparator;

        /***********************************************************************
         * STATIC BLOCK
         ***********************************************************************/
        static jcHistoryTable()
        {
            theInstance = new jcHistoryTable();
            History = new int[2][][];
            for (int i = 0; i < 2; i++)
            {
                History[i] = new int[64][];
                for (int j = 0; j < 64; j++)
                {
                    History[i][j] = new int[64];
                }
            }
        }

        public jcHistoryTable()
        {
            MoveComparator = new jcMoveComparator();
        }

        /***********************************************************************
         *  jcMoveComparator - Inner class used in sorting moves
         **********************************************************************/
        class jcMoveComparator : IComparer<jcMove>
        {
            public int Compare(jcMove mov1, jcMove mov2)
            {
                if (CurrentHistory[mov1.SourceSquare][mov1.DestinationSquare] >
                     CurrentHistory[mov2.SourceSquare][mov2.DestinationSquare])
                    return -1;
                else
                    return 1;
            }
        }

        /************************************************************************
         * PUBLIC METHODS
         ***********************************************************************/

        // Accessor
        public static jcHistoryTable GetInstance()
        {
            return theInstance;
        }

        // Sort a list of moves, using the Java "Arrays" class as a helper
        public bool SortMoveList(jcMoveListGenerator theList, int movingPlayer)
        {
            // Which history will we use?
            CurrentHistory = History[movingPlayer];

            // Arrays can't sort a dynamic array like jcMoveListGenerator's ArrayList
            // member, so we have to use an intermediate.  Annoying and not too clean,
            // but it works...
            theList.GetMoveList().Sort(MoveComparator);
            theList.ResetIterator();
            return true;
        }

        // History table compilation
        public bool AddCount(int whichPlayer, jcMove mov)
        {
            History[whichPlayer][mov.SourceSquare][mov.DestinationSquare]++;
            return true;
        }


        // public bool Forget
        // Once in a while, we must erase the history table to avoid ordering
        // moves according to the results of very old searches
        public bool Forget()
        {
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 64; j++)
                    for (int k = 0; k < 64; k++)
                        History[i][j][k] = 0;
            return true;
        }
    }
}
