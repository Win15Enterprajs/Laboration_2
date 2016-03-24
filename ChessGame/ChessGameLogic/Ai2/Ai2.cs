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
        Random rnd = new Random();
        //////////////////////////////////////////////////////
        private readonly List<Pieces> GameBoard;
        private List<Pieces> TempGameBoard;


        public Ai2(List<Pieces> gameBoard)
        {
            this.GameBoard = gameBoard;
            this.TempGameBoard = new List<Pieces>();
            MakeCopyOfGameboard();
            
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


                if (WillIthreaten(piece, move))
                {
                    move.value += 10;
                }
                if (WillIgetThreatened(move, piece))
                {
                    move.value -= 10;
                }
                if (AmIProtected(move, piece)) //ingen logic här i en.
                {
                    //nånting
                }
                if(CanIThreatenTheKing(move, piece))
                {
                    move.value += 25;
                }
                GiveRandomValueToAMove(move);
                PawnMoveToPromotion(piece);


                //RemoveSelfFromValue(move, piece); // funderar på hur denna metoden ska bestämma hur mycket som ska tas bort
                                                  // tänkte lite på att det kunde finnas en variabel som bestämmer hur många procent som skulle tas bort
            }                                     // som be ändras beronde på om (will i get threatned() protected() osv.. vi får ta en diskuterare


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










