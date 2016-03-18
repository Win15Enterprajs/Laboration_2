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
        List<string> GameLog;
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
                new Queen(Color.Black, new Point(4,7)),
                new King(Color.Black, new Point(3,7)),
                new Bishop(Color.Black, new Point(5,7)),
                new Horse(Color.Black, new Point(6,7)),
                new Rook(Color.Black, new Point(7,7), false)
                #endregion





            };


        }
        public List<Pieces> GetGameBoard()
        {
            return GameBoard;
        }
        public void MakeTurn()
        {
            InitializePiecesForThisTurn(GameBoard);
            GiveBestMoveToPieces();
            BestPiece = GetBestPiece(GameBoard);
            RemoveKilledPiece(BestPiece);
            BustAMove(BestPiece);
            BestPiece = null;
            turncounter++;

        }
        public void ThisIsIt()
        {
            PlayAGame(GameBoard);
        }

        private void GiveBestMoveToPieces()
        {

            Move bestMove = new Move(-1, -1, -999);
            foreach (Pieces piece in GameBoard)
            {
                if ((turncounter % 2) == 1 && piece.PieceColor == Color.White)
                {
                    foreach (var move in piece.ListOfMoves)
                    {
                        if (move.value > bestMove.value)
                            bestMove = move;

                    }
                }
                else if ((turncounter % 2) == 0 && piece.PieceColor == Color.Black)
                {
                    foreach (var move in piece.ListOfMoves)
                    {
                        if (move.value > bestMove.value)
                            bestMove = move;

                    }
                }
                var bPosX= bestMove.endPositions._PosX;
                var bPosY= bestMove.endPositions._PosY;
                var bVal = bestMove.value;

                if (piece.ListOfMoves.Count != 0)
                {
                    piece.BestMove = new Move(bPosX, bPosY, bVal); 
                }
                bestMove = new Move(-1, -1, -999);
            }

        }


        private void RemoveKilledPiece(Pieces piece)
        {
            int tempindex = 0;
            bool willweremove = false;
            for (int i = 0; i < GameBoard.Count; i++)
            {
                if (GameBoard[i].CurrentPosition._PosX == piece.BestMove.endPositions._PosX && GameBoard[i].CurrentPosition._PosY == piece.BestMove.endPositions._PosY)
                {
                    tempindex = i;
                    willweremove = true;
                }
                
            }
            if (willweremove)
            {
                GameBoard.RemoveAt(tempindex);
            }
            willweremove = false;
        }

        private void BustAMove(Pieces piece)
        {
            piece.CurrentPosition._PosX = piece.BestMove.endPositions._PosX;
            piece.CurrentPosition._PosY = piece.BestMove.endPositions._PosY;
            piece.hasBeenMoved = true;
        }
        private void InitializePiecesForThisTurn(List<Pieces> gameboard)
        {
            var Intelligence = new AI();
            var Movement = new MoveLogic();
            foreach (var item in gameboard)
            {
                Movement.SetMovementList(item, gameboard);
            }

            foreach (var item in gameboard)
            {
                Intelligence.GiveValuetoMoves(item,gameboard);
            }

        }
        public void PlayAGame(List<Pieces> gameboard)
        {
            
                var intelligence = new AI();
                var Movement = new MoveLogic();

                for (int i = 0; i < gameboard.Count; i++)
                {
                    if ((turncounter % 2) == 1 && gameboard[i].PieceColor == Color.White)
                    {
                        Movement.SetMovementList(gameboard[i], gameboard);
                    }
                    else if ((turncounter % 2) == 0 && gameboard[i].PieceColor == Color.Black)
                    {
                        Movement.SetMovementList(gameboard[i], gameboard);
                    }


                }
                for (int i = 0; i < gameboard.Count; i++)
                {
                    if ((turncounter % 2) == 1 && gameboard[i].PieceColor == Color.White)
                    {
                        intelligence.GiveValuetoMoves(gameboard[i],gameboard);
                    }
                    else if ((turncounter % 2) == 0 && gameboard[i].PieceColor == Color.Black)
                    {
                    intelligence.GiveValuetoMoves(gameboard[i], gameboard);
                    }

                }
                GiveBestMoveToPieces();
                BestPiece = GetBestPiece(gameboard);
                RemoveKilledPiece(BestPiece);
                BustAMove(BestPiece);
                ClearPieces();
                turncounter++;
             
        }
        private Pieces GetBestPiece(List<Pieces> gameboard)
        {

            Move bestMove = new Move(-1, -1, -999);
            var ListOfBestMovesWithSameValue = new List<Pieces>();

            for (int i = 0; i < gameboard.Count; i++)
            {
                if (gameboard[i].BestMove != null)
                {
                    if ((turncounter % 2) == 1 && gameboard[i].PieceColor == Color.White)
                    {
                        if (gameboard[i].BestMove.value >= bestMove.value)
                        {
                            bestMove.value = gameboard[i].BestMove.value;
                            ListOfBestMovesWithSameValue.Add(gameboard[i]);
                        }
                    }
                    else if ((turncounter % 2) == 0 && gameboard[i].PieceColor == Color.Black)
                    {
                        if (gameboard[i].BestMove.value >= bestMove.value)
                        {
                            bestMove.value = gameboard[i].BestMove.value;
                            ListOfBestMovesWithSameValue.Add(gameboard[i]);

                        }
                    } 
                }
            }
            var rnd = new Random();
            Pieces bestPiece = ListOfBestMovesWithSameValue.ElementAt(rnd.Next(0, ListOfBestMovesWithSameValue.Count));

            return bestPiece;
        }
        private void ClearPieces()
        {
            for (int i = 0; i < GameBoard.Count; i++)
            {
                GameBoard[i].ListOfMoves.Clear();
            }
        }





    }
}
