using System;
using System.IO;
using System.Threading;

namespace Chess.SmartAI
{
    public class jcGame
    {
        jcPlayer[] Players;
        jcBoard GameBoard;
        jcOpeningBook Openings;
    
        public bool InitializeGame(string openingBook, string startingPos)
        {
            Openings = new jcOpeningBook();
            Openings.Load(openingBook);

            var fen = string.Empty;
            using (var sr = new StreamReader(startingPos))
            {
                fen = sr.ReadLine();
            }

            var fenService = new FenService.FenService();
            var fenData = fenService.GetData(fen);

            GameBoard = new jcBoard();
            if (string.IsNullOrEmpty(startingPos))
                GameBoard.StartingBoard();
            else
                GameBoard.Load(fenData);

            Players = new jcPlayer[2];

            if (fenData.ColorToMove == Infrastructure.Enums.PieceColor.Black)
            {
                Players[jcPlayer.SIDE_BLACK] = new jcPlayerAI(jcPlayer.SIDE_BLACK, jcAISearchAgent.AISEARCH_ALPHABETA, Openings);
                Players[jcPlayer.SIDE_WHITE] = new jcPlayerHuman(jcPlayer.SIDE_WHITE);
            }
            else
            {
                Players[jcPlayer.SIDE_WHITE] = new jcPlayerAI(jcPlayer.SIDE_WHITE, jcAISearchAgent.AISEARCH_ALPHABETA, Openings);
                Players[jcPlayer.SIDE_BLACK] = new jcPlayerHuman(jcPlayer.SIDE_BLACK);
            }
            return true;
        }

        public bool RunGame()
        {
            jcPlayer CurrentPlayer;
            jcMove Mov;

            do
            {
                // Show the current game board
                GameBoard.Print();

                // Ask the next player for a move
                CurrentPlayer = Players[GameBoard.GetCurrentPlayer()];
                var timeStart = DateTime.Now;
                Mov = CurrentPlayer.GetMove(GameBoard);
                var timeEnd = DateTime.Now;

                Console.WriteLine("----------- MOVE DURATION: {0}s", (timeEnd - timeStart).TotalSeconds);

                Console.Write(jcPlayer.PlayerStrings[GameBoard.GetCurrentPlayer()]);
                Console.Write(" selects move: ");
                Mov.Print();

                // Change the state of the game accordingly
                GameBoard.ApplyMove(Mov);

            } while ((Mov.MoveType != jcMove.MOVE_RESIGN) &&
                     (Mov.MoveType != jcMove.MOVE_STALEMATE));

            Console.WriteLine("Game Over.  Thanks for playing!");

            return true;
        }
    }
}
