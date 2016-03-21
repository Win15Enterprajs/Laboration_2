using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic.ChessPieces
{
    class Ai2
    {
        private List<Pieces> Enemies;
        private List<Pieces> Allies;
        private List<Pieces> Gameboard;
        private List<Pieces> TempGameBoard;


        public void InitiateAI(Pieces piece, List<Pieces> board)
        {   
            Gameboard = board;
            TempGameBoard = new List<Pieces>(board);

            Allies = GetAllies(piece.PieceColor);
            Enemies = GetEnemies(piece.PieceColor);
        }
        public void GiveValueToMoves(Pieces piece, List<Pieces> board)
        {
           InitiateAI(piece,board);


        }

       


        private bool CanItakeSomething(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        private void GiveTakeValue(Pieces piece, List<Pieces> board)
        {
            
        }

        private bool WillIgetThreatened(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        private bool WillIBeProtected(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        private bool WillIthreaten(Pieces piece, List<Pieces> board)
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

        private List<Pieces> GetEnemies(Color color)
        {
            List<Pieces> listOfEnemies = new List<Pieces>();

            foreach (Pieces P in Gameboard)
            {
                if(P.PieceColor != color)
                    listOfEnemies.Add(P);
            }
            return listOfEnemies;
        }

        private List<Pieces> GetAllies(Color color)
        {
            List<Pieces> listOfAllies = new List<Pieces>();

            foreach (Pieces P in Gameboard)
            {
                if (P.PieceColor != color)
                    listOfAllies.Add(P);
            }
            return listOfAllies;
        }

        private void RestoreTempGameBoard()
        {
            TempGameBoard = new List<Pieces>(Gameboard);
        }


    }
}
