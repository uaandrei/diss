namespace Chess.SmartAI
{
    public class jcPlayerAI : jcPlayer
    {
        /************************************************************************
         * DATA MEMBERS
         ***********************************************************************/

        // The search agent in charge of the moves
        jcAISearchAgent Agent;

        /***********************************************************************
         * PUBLIC METHODS
         **********************************************************************/

        // Constructor
        public jcPlayerAI(int whichPlayer, int whichType, jcOpeningBook refBook)
        {
            this.SetSide(whichPlayer);
            Agent = jcAISearchAgent.MakeNewAgent(whichType, refBook);
        }

        // Attach a search agent to the AI player
        public bool AttachSearchAgent(jcAISearchAgent theAgent)
        {
            Agent = theAgent;
            return true;
        }

        // Getting a move from the machine
        public override jcMove GetMove(jcBoard theBoard)
        {
            return (Agent.PickBestMove(theBoard));
        }
    }
}
