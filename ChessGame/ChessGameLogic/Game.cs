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
                #region Adds White Player Pieces
                new Pawn(Color.White, new Point(0,1), false),
                new Pawn(Color.White, new Point(1,1), false),
                new Pawn(Color.White, new Point(2,1), false),
                new Pawn(Color.White, new Point(3,1), false),
                new Pawn(Color.White, new Point(4,1), false),
                new Pawn(Color.White, new Point(5,1), false),
                new Pawn(Color.White, new Point(6,1), false),
                new Pawn(Color.White, new Point(7,1), false),
                new Rook(Color.White, new Point(0,0), false),
                new Horse(Color.White, new Point(1,0)),
                new Bishop(Color.White, new Point(2,0)),
                new Queen(Color.White, new Point(3,0)),
                new King(Color.White, new Point(4,0)),
                new Bishop(Color.White, new Point(5,0)),
                new Horse(Color.White, new Point(6,0)),
                new Rook(Color.White, new Point(7,0), false),
                #endregion
                #region Adds Black Player Pieces
                new Pawn(Color.Black, new Point(0,6), false),
                new Pawn(Color.Black, new Point(1,6), false),
                new Pawn(Color.Black, new Point(2,6), false),
                new Pawn(Color.Black, new Point(3,6), false),
                new Pawn(Color.Black, new Point(4,6), false),
                new Pawn(Color.Black, new Point(5,6), false),
                new Pawn(Color.Black, new Point(6,6), false),
                new Pawn(Color.Black, new Point(7,6), false),
                new Rook(Color.Black, new Point(0,7), false),
                new Horse(Color.Black, new Point(1,7)),
                new Bishop(Color.Black, new Point(2,7)),
                new Queen(Color.Black, new Point(3,7)),
                new King(Color.Black, new Point(4,7)),
                new Bishop(Color.Black, new Point(5,7)),
                new Horse(Color.Black, new Point(6,7)),
                new Rook(Color.Black, new Point(7,7), false),
                #endregion





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
                else if (turncounter % 2 == 0 && piece.PieceColor == Color.Black)
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
