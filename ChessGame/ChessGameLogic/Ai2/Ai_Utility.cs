﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
   partial class Ai2
    {
        public void InitiateAI(Color color)
        {   //creates the gameboard that the Ai will use as reference. 
            ///////////////////////////////////////////// 

            TempGameBoard = new List<Pieces>(GameBoard);

            /////////////////////////////////////////////
            // seperates the allies and enemies into different lists. (for simplicity)
            Allies = GetAllies(color);
            Enemies = GetEnemies(color);
            SetMovesForEnemies();
        }

        // om man gör ändringar för att kolla olika vilkor, tex (willthis move threaten something) så kan man resetta tempgameboard till "gameBoard"
        private void RestoreTempGameBoard()
        {
            TempGameBoard = new List<Pieces>(GameBoard);
        }


        // eftersom ennemy inte har några moves, så får dom de här! 
        private void SetMovesForEnemies()
        {
            foreach (Pieces P in Enemies)
            {
                AiMove.SetMovementList(P, TempGameBoard);
            }
        }


        private Pieces GetPieceFromTempBoard(Pieces piece)
        {
            Pieces tempPiece = TempGameBoard.Find(p => p.CurrentPosition == piece.CurrentPosition);
            return tempPiece;
        }
        // gämnför två Moves och returnerar true eller false beroende på om de innehåller samma koordinater eller inte. 
        #region CompareMoves()...
        public bool CompareEnemyMoveToMyMove(Move enemyMove, Move move)
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
        public bool CompareEnemyPositionToMyMove(Point enemyPosition, Move move)
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
        public bool CompareEnemyMoveWithCurrentPosition(Move enemyMove, Point myPosition)
        {
            if (enemyMove.endPositions._PosX == myPosition._PosX && enemyMove.endPositions._PosY == myPosition._PosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        // separerar ennemies från allies, lägger dom i egna listor(för enkelhets skull. gör också så att man inte behöver ha fula checkar över allt)
        #region Seperate Enemies and Allies
        private List<Pieces> GetEnemies(Color color)
        {
            return GameBoard.Where(P => P.PieceColor != color).ToList();
        }
        private List<Pieces> GetAllies(Color color)
        {
            return GameBoard.Where(P => P.PieceColor == color).ToList();
        }
        #endregion
    }
}
