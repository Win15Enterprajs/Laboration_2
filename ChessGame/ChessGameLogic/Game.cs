using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;
using System.Threading;

namespace ChessGameLogic
{
    /// <summary>
    /// The Game class.
    /// This Class is where the game is ran.
    /// The gameboard is also stored here in the form of a list.
    /// It includes instances of MoveLogic and AI classes which calculates moves, and values of those moves for each piece.
    /// Then Methods in the gameclass chooses the best move based on values and executes it.    /// 
    /// </summary>
    public class Game
    {
        int turncounter = 1;
        private int noTakeTurns = 0;
        private GameState state;
        List<Pieces> GameBoard;
        GameLogger logger;
        Pieces BestPiece;
        Ai2 intelligence;
        MoveLogic movement;
        Random rnd;
        
        /// <summary>
        /// State of the game.
        /// </summary>
        public GameState State
        {
            get { return state; }
            set { state = value; }
        }

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
                new Rook(Color.Black, new Point(7,7), false)
                #endregion

            };
            logger = new GameLogger();
            state = GameState.Running;
            intelligence = new Ai2(this.GameBoard);
            movement = new MoveLogic();
            rnd = new Random();


        }

        /// <summary>
        /// A method that returns which turn it is for the UI.
        /// </summary>
        /// <returns>Turn</returns>
        public int GetTurn()
        {
            return turncounter;
        }

        /// <summary>
        /// gets the gameboard so that the GUI can print it.
        /// </summary>
        /// <returns>List<Pieces> Gameboard</Pieces></returns>
        public List<Pieces> GetGameBoard()
        {
            return GameBoard;
        }
        /// <summary>
        /// Gets the Gamelog.
        /// </summary>
        /// <returns>List of strings Gamelog</returns>
        public List<string> getGameLog()
        {
            return logger.gameLog;
        }
        /// <summary>
        /// Goes through all pieces moves and assign the highest value move to the piece bestmove.
        /// </summary>
        private void GiveBestMoveToPieces() 
        {

            double bestMoveValue = -99;
            List<Move> newListOfBestMoves = new List<Move>();
            foreach (Pieces piece in GameBoard)
            {
                if ((turncounter % 2) == 1 && piece.PieceColor == Color.White)
                {
                    foreach (var move in piece.ListOfMoves)
                    {
                        if (move.value >= bestMoveValue)
                        {
                            bestMoveValue = move.value;
                            newListOfBestMoves.Add(new Move(move.endPositions, move.value));
                        }
                    }
                }
                else if ((turncounter % 2) == 0 && piece.PieceColor == Color.Black)
                {
                    foreach (var move in piece.ListOfMoves)
                    {
                        if (move.value >= bestMoveValue)
                        {
                            bestMoveValue = move.value;
                            newListOfBestMoves.Add(new Move(move.endPositions, move.value));
                        }
                    }
                }

                if (piece.ListOfMoves.Count != 0 && newListOfBestMoves.Count != 0)
                {
                    var listOfBestMoves = newListOfBestMoves.Where(x => x.value == bestMoveValue).ToList();
                    piece.BestMove = listOfBestMoves.ElementAt(rnd.Next(0, listOfBestMoves.Count));
                    newListOfBestMoves.Clear();

                }
                //bestMove = new Move(-1, -1, -999);
            }

        }

        /// <summary>
        /// Removes a piece from the gameboard if another piece has taken it.
        /// </summary>
        /// <param name="piece">The piece that potentially can take something.</param>
        private void RemoveKilledPiece(Pieces piece)
        {
            int tempindex = 0;
            bool willweremove = false;
            if (piece.BestMove != null)
            {
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
                    logger.LogKilledPieceToRemove(GameBoard[tempindex]);
                    GameBoard.RemoveAt(tempindex);
                    noTakeTurns = 0;
                }
                else
                {
                    noTakeTurns++;
                }
                willweremove = false; 
            }
        }

        /// <summary>
        /// Makes the best move this turn.
        /// </summary>
        /// <param name="piece">The piece which to move.</param>
        private void MakeAMove(Pieces piece)
        {
            if (piece.BestMove != null)
            {
                piece.CurrentPosition._PosX = piece.BestMove.endPositions._PosX;
                piece.CurrentPosition._PosY = piece.BestMove.endPositions._PosY;
                if(piece is Pawn)
                {
                    if (piece.PieceColor == Color.Black && piece.BestMove.endPositions._PosY == 0)
                    {
                        var QueenFromPawn = new Queen(Color.Black, new Point(piece.CurrentPosition._PosX, piece.CurrentPosition._PosY));
                        GameBoard.Remove(piece);
                        GameBoard.Add(QueenFromPawn);
                    }
                    else if(piece.BestMove.endPositions._PosY == 7)
                    {
                        var QueenFromPawn = new Queen(Color.White, new Point(piece.CurrentPosition._PosX, piece.CurrentPosition._PosY));
                        GameBoard.Remove(piece);
                        GameBoard.Add(QueenFromPawn);
                    }
                }
                piece.hasBeenMoved = true; 
            } 

        }

        /// <summary>
        /// The method which is called from the gui to progress the game one turn.
        /// </summary>
        public void PlayGame()
        {
            CalculateMovesAndValues();
            GiveBestMoveToPieces();
            BestPiece = GetBestPiece(GameBoard);
            RemoveKilledPiece(BestPiece);
            MakeAMove(BestPiece);
            logger.LogTurn();
            EvaluateStateOfGame();
            ClearPieces();
            turncounter++;

        }

        /// <summary>
        /// Goes through all pieces bestmoves and selects the one with highest value.
        /// </summary>
        /// <param name="gameboard"></param>
        /// <returns></returns>
        private Pieces GetBestPiece(List<Pieces> gameboard)
        {
            
            double bestMoveValue = -99;
            var listOfMoves = new List<Pieces>();

            for (int i = 0; i < gameboard.Count; i++)
            {
                if (gameboard[i].BestMove != null)
                {
                    if ((turncounter % 2) == 1 && gameboard[i].PieceColor == Color.White)
                    {
                        if (gameboard[i].BestMove.value >= bestMoveValue)
                        {
                            bestMoveValue = gameboard[i].BestMove.value;
                            listOfMoves.Add(gameboard[i]);
                        }
                    }
                    else if ((turncounter % 2) == 0 && gameboard[i].PieceColor == Color.Black)
                    {
                        if (gameboard[i].BestMove.value >= bestMoveValue)
                        {
                            bestMoveValue = gameboard[i].BestMove.value;
                            listOfMoves.Add(gameboard[i]);

                        }
                    } 
                }
            }

            if (listOfMoves.Count != 0)
            {
                var listOfBestMoves = listOfMoves.Where(x => x.BestMove.value == bestMoveValue).ToList();
                var bestPiece = listOfBestMoves.ElementAt(rnd.Next(0, listOfBestMoves.Count));
                logger.LogPieceToMove(bestPiece);
                return bestPiece; 
            }
            return BestPiece;
        }

        /// <summary>
        /// Clears all the pieces list of moves so they are ready for the next turn.
        /// </summary>
        private void ClearPieces()
        {
            for (int i = 0; i < GameBoard.Count; i++)
            {
                GameBoard[i].ListOfMoves.Clear();
                GameBoard[i].BestMove = null;

            }
           
        }

        /// <summary>
        /// A method which checks if the enemy is in check next turn.
        /// </summary>
        /// <returns></returns>
        private bool WillThisTurnPutEnemyInCheck()
        {
            return movement.IsEnemyInCheck(turncounter, GameBoard);
        }

        /// <summary>
        /// This method evaluates and sets the state of the game after each round.
        /// </summary>
        private void EvaluateStateOfGame()
        {
            int PiecesLeftThatCanCheckMate = GameBoard.Count(x => (x is Queen || x is Rook || x is Pawn));
            bool checkThisRound = movement.CheckGamestateForCheck(turncounter, GameBoard);

            if (BestPiece.BestMove == null && !checkThisRound)
            {
                state = GameState.Draw;
            }
            else if (BestPiece.BestMove == null && checkThisRound)
            {
                state = GameState.Checkmate;
            }

            else if (GameBoard.Count == 2 || (GameBoard.Count < 4 && (PiecesLeftThatCanCheckMate == 0 ) || (!GameBoard.Exists(x => (x is Pawn || x is Rook || x is Queen)) && noTakeTurns > 50)))
            {
                state = GameState.Draw;
            }
            else if (WillThisTurnPutEnemyInCheck())
            {
                state = GameState.Check;
            }
            else
            {
                state = GameState.Running;
            }
        }

        /// <summary>
        /// Calculates every pieces possible moves and gives them values by putting each pieced through MoveLogic and the AI
        /// </summary>
        private void CalculateMovesAndValues()
        {
            for (int i = 0; i < GameBoard.Count; i++)
            {
                if ((turncounter % 2) == 1 && GameBoard[i].PieceColor == Color.White)
                {
                    movement.SetMovementList(GameBoard[i], GameBoard);
                }
                else if ((turncounter % 2) == 0 && GameBoard[i].PieceColor == Color.Black)
                {
                    movement.SetMovementList(GameBoard[i], GameBoard);
                }


            }
            for (int i = 0; i < GameBoard.Count; i++)
            {
                if ((turncounter % 2) == 1 && GameBoard[i].PieceColor == Color.White)
                {
                    intelligence.GiveValueToMoves(GameBoard[i]);
                }
                else if ((turncounter % 2) == 0 && GameBoard[i].PieceColor == Color.Black)
                {
                    intelligence.GiveValueToMoves(GameBoard[i]);
                }

            }
        }

       




    }
}
