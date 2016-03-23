using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{

    partial class Ai2
    {
        MoveLogic AiMove = new MoveLogic();
        private List<Pieces> Enemies;
        private List<Pieces> Allies;
        //////////////////////////////////////////////////////
        private readonly List<Pieces> GameBoard;
        private List<Pieces> TempGameBoard;


        public Ai2(List<Pieces> gameBoard)
        {
            this.GameBoard = gameBoard;
        }



        //bas Metoden.. typish.. 
        public void GiveValueToMoves(Pieces piece)
        {
            InitiateAI(piece.PieceColor);
            foreach (Move move in piece.ListOfMoves)
            {
                if (CanItakeSomething(move))
                {
                    GiveTakeValue(move);
                }


                WillIthreaten(piece, move);
                if (WillIgetThreatened(move, piece))
                {

                    if (AmIProtected(move, piece)) //ingen logic här i en.
                    {
                        RemoveSelfFromValue(move, piece);
                    }
                }
            }


        }


        //kollar bara om ett moves kan ta något
        private bool CanItakeSomething(Move move)
        {
            foreach (Pieces E in Enemies)
            {

                if (CompareEnemyPositionToMyMove(E.CurrentPosition, move))
                {
                    return true;
                }
            }
            return false;
        }

        // ger movet värde beroende på vad den tar
        private void GiveTakeValue(Move move)
        {
            double enemyValue = GetThretnedEnemyPiece(move).Value;
            move.value += enemyValue;

        }

        private void RemoveSelfFromValue(Move move, Pieces piece)
        {
            move.value -= piece.Value;
        }

        // precis som det låter, kollar om movet kommer bli hotat av en motståndare










    }
}










