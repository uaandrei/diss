using System;
namespace Chess.SmartAI {
	public class jcPlayerHuman : jcPlayer {
		// The keyboard
		char[] linebuf;

		// Validation help
		jcMoveListGenerator Pseudos;
		jcBoard Successor;

		// Constructor
		public jcPlayerHuman(int which) {
			this.SetSide(which);
			linebuf = new char[10];
			Pseudos = new jcMoveListGenerator();
			Successor = new jcBoard();
		}

		// public jcMove GetMove( theBoard )
		// Getting a move from the human player.  Sorry, but this is very, very
		// primitive: you need to enter square numbers instead of piece ID's, and
		// both square numbers must be entered with two digits.  Ex.: 04 00
		public override jcMove GetMove(jcBoard theBoard) {
			// Read the move from the command line
			bool ok = false;
			jcMove Mov = new jcMove();
			do {
				Console.WriteLine("Your move, " + PlayerStrings[this.GetSide()] + "?");

				// Get data from the command line
				string line = null;
				do {
					try {
						line = Console.ReadLine();
					} catch(Exception) { }
				} while(line.Length < 3);

				if(line.ToUpper().Equals("RESIG")) {
					Mov.MoveType = jcMove.MOVE_RESIGN;
					return (Mov);
				}

				// Extract the source and destination squares from the line buffer
				Mov.SourceSquare = Convert.ToInt32(line.Substring(0, 2));
				Mov.DestinationSquare = Convert.ToInt32(line.Substring(3, 2));
				if((Mov.SourceSquare < 0) || (Mov.SourceSquare > 63)) {
					Console.WriteLine("Sorry, illegal source square " + Mov.SourceSquare);
					continue;
				}
				if((Mov.DestinationSquare < 0) || (Mov.DestinationSquare > 63)) {
					Console.WriteLine("Sorry, illegal destination square " + Mov.DestinationSquare);
					continue;
				}

				// Time to try to figure out what the move means!
				if(theBoard.GetCurrentPlayer() == jcPlayer.SIDE_WHITE) {
					// Is there a piece (of the moving player) on SourceSquare?
					// If not, abort
					Mov.MovingPiece = theBoard.FindWhitePiece(Mov.SourceSquare);
					if(Mov.MovingPiece == jcBoard.EMPTY_SQUARE) {
						Console.WriteLine("Sorry, You don't have a piece at square " + Mov.SourceSquare);
						continue;
					}

					// Three cases: there is a piece on the destination square (a capture),
					// the destination square allows an en passant capture, or it is a
					// simple non-capture move.  If the destination contains a piece of the
					// moving side, abort
					if(theBoard.FindWhitePiece(Mov.DestinationSquare) != jcBoard.EMPTY_SQUARE) {
						Console.WriteLine("Sorry, can't capture your own piece!");
						continue;
					}
					Mov.CapturedPiece = theBoard.FindBlackPiece(Mov.DestinationSquare);
					if(Mov.CapturedPiece != jcBoard.EMPTY_SQUARE)
						Mov.MoveType = jcMove.MOVE_CAPTURE_ORDINARY;
					else if((theBoard.GetEnPassantPawn() == (1 << Mov.DestinationSquare)) &&
							  (Mov.MovingPiece == jcBoard.WHITE_PAWN)) {
						Mov.CapturedPiece = jcBoard.BLACK_PAWN;
						Mov.MoveType = jcMove.MOVE_CAPTURE_EN_PASSANT;
					}

					// If the move isn't a capture, it may be a castling attempt
					else if((Mov.MovingPiece == jcBoard.WHITE_KING) &&
							  ((Mov.SourceSquare - Mov.DestinationSquare) == 2))
						Mov.MoveType = jcMove.MOVE_CASTLING_KINGSIDE;
					else if((Mov.MovingPiece == jcBoard.WHITE_KING) &&
							  ((Mov.SourceSquare - Mov.DestinationSquare) == -2))
						Mov.MoveType = jcMove.MOVE_CASTLING_QUEENSIDE;
					else
						Mov.MoveType = jcMove.MOVE_NORMAL;
				} else {
					Mov.MovingPiece = theBoard.FindBlackPiece(Mov.SourceSquare);
					if(Mov.MovingPiece == jcBoard.EMPTY_SQUARE) {
						Console.WriteLine("Sorry, you don't have a piece in square " + Mov.SourceSquare);
						continue;
					}

					if(theBoard.FindBlackPiece(Mov.DestinationSquare) != jcBoard.EMPTY_SQUARE) {
						Console.WriteLine("Sorry, you can't capture your own piece in square " + Mov.DestinationSquare);
						continue;
					}
					Mov.CapturedPiece = theBoard.FindWhitePiece(Mov.DestinationSquare);
					if(Mov.CapturedPiece != jcBoard.EMPTY_SQUARE)
						Mov.MoveType = jcMove.MOVE_CAPTURE_ORDINARY;
					else if((theBoard.GetEnPassantPawn() == (1 << Mov.DestinationSquare)) &&
							  (Mov.MovingPiece == jcBoard.BLACK_PAWN)) {
						Mov.CapturedPiece = jcBoard.WHITE_PAWN;
						Mov.MoveType = jcMove.MOVE_CAPTURE_EN_PASSANT;
					} else if((Mov.MovingPiece == jcBoard.BLACK_KING) &&
								((Mov.SourceSquare - Mov.DestinationSquare) == 2))
						Mov.MoveType = jcMove.MOVE_CASTLING_KINGSIDE;
					else if((Mov.MovingPiece == jcBoard.BLACK_KING) &&
							  ((Mov.SourceSquare - Mov.DestinationSquare) == -2))
						Mov.MoveType = jcMove.MOVE_CASTLING_QUEENSIDE;
					else
						Mov.MoveType = jcMove.MOVE_NORMAL;
				}

				// Now, if the move results in a pawn promotion, we must ask the user
				// for the type of promotion!
				if(((Mov.MovingPiece == jcBoard.WHITE_PAWN) && (Mov.DestinationSquare < 8)) ||
					 ((Mov.MovingPiece == jcBoard.BLACK_PAWN) && (Mov.DestinationSquare > 55))) {
					int car = -1;
					Console.WriteLine("Promote the pawn to [K]night, [R]ook, [B]ishop, [Q]ueen?");
					do {
						try { car = Console.Read(); } catch(Exception) { }
					} while((car != 'K') && (car != 'k') && (car != 'b') && (car != 'B')
						   && (car != 'R') && (car != 'r') && (car != 'Q') && (car != 'q'));
					if((car == 'K') || (car == 'k'))
						Mov.MoveType += jcMove.MOVE_PROMOTION_KNIGHT;
					else if((car == 'B') || (car == 'b'))
						Mov.MoveType += jcMove.MOVE_PROMOTION_BISHOP;
					else if((car == 'R') || (car == 'r'))
						Mov.MoveType += jcMove.MOVE_PROMOTION_ROOK;
					else
						Mov.MoveType += jcMove.MOVE_PROMOTION_QUEEN;
				}

				// OK, now let's see if the move is actually legal!  First step: a check
				// for pseudo-legality, i.e., is it a valid successor to the current
				// board?
				Pseudos.ComputeLegalMoves(theBoard);
				if(!Pseudos.Find(Mov)) {
					Console.Write("Sorry, this move is not in the pseudo-legal list: ");
					Mov.Print();
					Pseudos.Print();
					continue;
				}

				// If pseudo-legal, then verify whether it leaves the king in check
				Successor.Clone(theBoard);
				Successor.ApplyMove(Mov);
				if(!Pseudos.ComputeLegalMoves(Successor)) {
					Console.Write("Sorry, this move leaves your king in check: ");
					Mov.Print();
					continue;
				}

				// If we have made it here, we have a valid move to play!
				Console.WriteLine("Move is accepted...");
				ok = true;

			} while(!ok);

			return (Mov);
		}
	}
}
