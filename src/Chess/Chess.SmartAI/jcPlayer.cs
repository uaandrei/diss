namespace Chess.SmartAI
{
    abstract public class jcPlayer
    {
        /***************************************************************************
         * Constants
         **************************************************************************/

        public const int SIDE_BLACK = 1;
        public const int SIDE_WHITE = 0;
        public static readonly string[] PlayerStrings = { "WHITE", "BLACK" };

        // Data member: which side is this player representing?
        protected int Side;

        // Constructor
        public jcPlayer()
        {
        }

        // Accessors
        protected int GetSide()
        {
            return Side;
        }
        protected void SetSide(int s)
        {
            Side = s;
        }

        // abstract jcMove GetMove()
        // Ask the player to provide a move, given the current board situation
        public abstract jcMove GetMove(jcBoard theBoard);
    }
}