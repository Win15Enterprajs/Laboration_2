using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic.ChessPieces
{

    partial class Ai2
    {
        MoveLogic AiMove = new MoveLogic();
        private List<Pieces> Enemies;
        private List<Pieces> Allies;
        //////////////////////////////////////////////////////
        private List<Pieces> GameBoard;
        private List<Pieces> TempGameBoard;


        public Ai2(List<Pieces> gameBoard)
        {
            
        }

        public void InitiateAI(Pieces piece)
        {   //creates the gameboard that the Ai will use as reference. 
            ///////////////////////////////////////////// 
           
            TempGameBoard = new List<Pieces>(GameBoard);

            /////////////////////////////////////////////
            // seperates the allies and enemies into different lists. (for simplicity)
            Allies = GetAllies(piece.PieceColor);
            Enemies = GetEnemies(piece.PieceColor);
            SetMovesForEnemies();
        }
        public void GiveValueToMoves(Pieces piece)
        {
            foreach (Move move in piece.ListOfMoves)
            {
                if (CanItakeSomething(move))
                {
                    GiveTakeValue(move);

                    WillIgetThreatened(move);
                }
            }
        }

        private bool CanItakeSomething(Move move)
        {
            foreach (Pieces E in Enemies)
            {
                foreach (Move Emove in E.ListOfMoves)
                {
                    if (CompareMoves(Emove, move))
                        return true;
                }
            }
            return false;
        }

        private void GiveTakeValue(Move move)
        {
            foreach (Pieces enemy in Enemies)
            {
                if (CompareMoves(enemy.CurrentPosition, move))
                {
                    move.value += enemy.Value;
                }
            }
        }

        private bool WillIgetThreatened(Move move)
        {

            return false;
        }

        private bool WillIBeProtected(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        private bool WillIthreaten(Move move)
        {
            return false;
        }

        private bool AmIProtected(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        private bool AmIThreatened(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        private bool CanIThreatenDaKing()
        {
            return false;
        }

        private List<Pieces> GetEnemies(Color color)
        {
            return GameBoard.Where(P => P.PieceColor != color).ToList();
        }

        private List<Pieces> GetAllies(Color color)
        {
            return GameBoard.Where(P => P.PieceColor == color).ToList();
        }

        private void RestoreTempGameBoard()
        {
            TempGameBoard = new List<Pieces>(GameBoard);
        }

        private void SetMovesForEnemies()
        {
            foreach (Pieces P in Enemies)
            {
                AiMove.SetMovementList(P, GameBoard);
            }
        }

        #region CompareMoves()...
        public bool CompareMoves(Move enemyMove, Move move)
        {
            if (enemyMove.endPositions._PosX == move.endPositions._PosX && enemyMove.endPositions._PosY == move.endPositions._PosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CompareMoves(Point enemyPosition, Move move)
        {
            if (enemyPosition._PosX == move.endPositions._PosX && enemyPosition._PosY == move.endPositions._PosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion



    }
}










