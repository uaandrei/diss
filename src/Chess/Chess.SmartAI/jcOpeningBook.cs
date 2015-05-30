using System;
using System.IO;
namespace Chess.SmartAI
{
    class jcOpeningBookEntry
    {
        // A signature for the board position stored in the entry
        public int theLock;

        // Moves
        public jcMove WhiteMove;
        public jcMove BlackMove;

        // A sentinel indicating that a move is invalid
        public const int NO_MOVE = -1;

        // Construction
        public jcOpeningBookEntry()
        {
            theLock = 0;
            WhiteMove = new jcMove();
            WhiteMove.MoveType = NO_MOVE;
            BlackMove = new jcMove();
            BlackMove.MoveType = NO_MOVE;
        }
    }


    /*****************************************************************************
     * PUBLIC class jcOpeningBook
     * A hash table containing a certain number of slots for well-known positions
     ****************************************************************************/

    public class jcOpeningBook
    {
        // The hash table itself
        private const int TABLE_SIZE = 1024;
        private jcOpeningBookEntry[] Table;

        // Construction
        public jcOpeningBook()
        {
            Table = new jcOpeningBookEntry[TABLE_SIZE];
            for (int i = 0; i < TABLE_SIZE; i++)
            {
                Table[i] = new jcOpeningBookEntry();
            }
        }

        // public jcMove Query
        // Querying the table for a ready-made move to play.  Return null if there
        // is none
        public jcMove Query(jcBoard theBoard)
        {
            // First, look for a match in the table
            int key = Math.Abs(theBoard.HashKey() % TABLE_SIZE);
            int hashLock = theBoard.HashLock();

            // If the hash lock doesn't match the one for our position, get out
            if (Table[key].theLock != hashLock)
                return null;

            // If there is an entry for this board in the table, verify that it
            // contains a move for the current side
            if (theBoard.GetCurrentPlayer() == jcPlayer.SIDE_BLACK)
            {
                if (Table[key].BlackMove.MoveType != jcOpeningBookEntry.NO_MOVE)
                    return Table[key].BlackMove;
            }
            else
            {
                if (Table[key].WhiteMove.MoveType != jcOpeningBookEntry.NO_MOVE)
                    return Table[key].WhiteMove;
            }

            // If we haven't found anything useful, quit
            return null;
        }

        // Loading the table from a file
        public bool Load(string fileName)
        {
            // Open the file as a Java tokenizer
            using (var sr = new StreamReader(fileName))
            {
                // Create a game board on which to "play" the opening sequences stored in
                // the book, so that we know which position to associate with which move
                jcBoard board = new jcBoard();
                jcMove mov = new jcMove();
                jcMoveListGenerator successors = new jcMoveListGenerator();

                // How many lines of play do we have in the book?
                var lines = File.ReadAllLines(fileName);
                int numLines = lines.Length;

                for (int wak = 0; wak < numLines; wak++)
                {
                    // Begin the line of play with a clean board
                    board.StartingBoard();

                    // Load the continuation
                    var lineNums = lines[wak].Split(' ');
                    for (int i = 0; i < lineNums.Length; i++)
                    {
                        if (lineNums[i] == "END")
                            break;
                        successors.ComputeLegalMoves(board);

                        var source = Convert.ToInt32(lineNums[i]);
                        var destination = Convert.ToInt32(lineNums[i + 1]);
                        i++;

                        mov = successors.FindMoveForSquares(source, destination);
                        StoreMove(board, mov);
                        board.ApplyMove(mov);

                    }
                }

            }
            return true;
        }


        private bool StoreMove(jcBoard theBoard, jcMove theMove)
        {
            // Where should we store this data?
            int key = Math.Abs(theBoard.HashKey() % TABLE_SIZE);
            int hashLock = theBoard.HashLock();

            // Is there already an entry for a different board position where we
            // want to put this?  If so, mark it deleted
            if (Table[key].theLock != hashLock)
            {
                Table[key].BlackMove.MoveType = jcOpeningBookEntry.NO_MOVE;
                Table[key].WhiteMove.MoveType = jcOpeningBookEntry.NO_MOVE;
            }

            // And store the new move
            Table[key].theLock = hashLock;
            if (theBoard.GetCurrentPlayer() == jcPlayer.SIDE_BLACK)
            {
                Table[key].BlackMove.Copy(theMove);
            }
            else
            {
                Table[key].WhiteMove.Copy(theMove);
            }

            return true;
        }
    }
}