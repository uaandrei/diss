using System;
namespace Chess.SmartAI {

	public class jcApp {
		public static void Main(params string[] args) {
			// Extract the parameters
			string openingBook = "openings.txt";
			string startingPos = "fen1.txt";

			// Initialize the game controller
			jcGame theGame = new jcGame();
			try {
				theGame.InitializeGame(openingBook, startingPos);
			} catch(Exception e) {
				Console.WriteLine(e.StackTrace);
			}

			// Run the game
			try {
				theGame.RunGame();
			} catch(Exception e) {
				Console.WriteLine(e.StackTrace);
			}
		}
	}
}
