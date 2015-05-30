namespace Chess.SmartAI {
	public class jcAISearchAgentMDTF : jcAISearchAgent {

		// Construction
		public jcAISearchAgentMDTF() {
			Evaluator = new jcBoardEvaluator();
		}

		// Move selection
		public override jcMove PickBestMove(jcBoard theBoard) {
			// FDL Do the real work later
			return (new jcMove());
		}
	}
}
