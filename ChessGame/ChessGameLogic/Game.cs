using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    public class Game
    {
        int turncounter = 0;
        List<Pieces> GameBoard;
        Pieces BestPiece;
        Player Player1;
        Player Player2;



        public Game()
        {
            GameBoard = new List<Pieces>()
            {
                // Creating the white pieces
                new Pawn(Color.White, new Point(0,1), false),
                new Pawn(Color.White, new Point(1,1), false),
                new Pawn(Color.White, new Point(2,1), false),
                new Pawn(Color.White, new Point(3,1), false),
                new Pawn(Color.White, new Point(4,1), false),
                new Pawn(Color.White, new Point(5,1), false),
                new Pawn(Color.White, new Point(6,1), false),
                new Pawn(Color.White, new Point(7,1), false),

                // Creating the black pieces

                new Pawn(Color.White, new Point(0,1), false),
                new Pawn(Color.White, new Point(1,1), false),
                new Pawn(Color.White, new Point(2,1), false),
                new Pawn(Color.White, new Point(3,1), false),
                new Pawn(Color.White, new Point(4,1), false),
                new Pawn(Color.White, new Point(5,1), false),
                new Pawn(Color.White, new Point(6,1), false),
                new Pawn(Color.White, new Point(7,1), false),
            };
        }
        public List<Pieces> GetGameBoard()
        {
            return GameBoard;
        }
        public void MakeTurn()
        {
            GiveBestMoveToPieces();
            Pieces PieceToMove = GetBestPiece();
            RemoveKilledPiece(PieceToMove);
            BustAMove(PieceToMove);
            turncounter++;

            Console.WriteLine("OOOOOH YEEEA!!!");

        }

        private void GiveBestMoveToPieces()
        {

            Move bestMove = new Move(-1, -1, -999);
            foreach (Pieces piece in GameBoard)
            {
                if (turncounter % 2 == 1 && piece.PieceColor == Color.White)
                foreach (var move in piece.ListOfMoves)
                {
                    if (move.value > bestMove.value)
                        bestMove = move;

                }
                else if (turncounter %  2 == 1 && piece.PieceColor == Color.Black)
                {
                    foreach (var move in piece.ListOfMoves)
                    {
                        if (move.value > bestMove.value)
                            bestMove = move;

                    }
                }
                piece.BestMove = bestMove;
            }

        }

        private Pieces GetBestPiece()
        {

            Move bestMove = new Move(-1, -1, -999);
            Pieces bestPiece = null;


            foreach (Pieces piece in GameBoard)
            {

                if (piece.BestMove.value > bestMove.value)
                {

                    bestPiece = piece;
                }
            }
            return bestPiece;
        }

        private void RemoveKilledPiece(Pieces piece)
        {
            for (int i = 0; i < GameBoard.Count; i++)
            {
                if (GameBoard[i].CurrentPosition == piece.CurrentPosition)
                    GameBoard.RemoveAt(i);
            }
        }

        private void BustAMove(Pieces piece)
        {
            GameBoard.Add(piece);
        }






    }
}
