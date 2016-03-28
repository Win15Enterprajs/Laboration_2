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

        /// <summary>
        /// Public base method that sets the value to all moves
        /// </summary>
        /// <param name="piece"></param>
        public void GiveValueToMoves(Pieces piece)
        {
            InitiateAI(piece.PieceColor);

            var valuedListOfMoves = new List<Move>();
            foreach (Move move in piece.ListOfMoves)
            {
                var valuedMove = new Move(move.endPositions._PosX, move.endPositions._PosY, 0);
                if (CanItakeSomething(move))
                {
                    GiveTakeValue(valuedMove, piece);
                }             
                if (WillIthreaten(piece, move))
                {
                    valuedMove.value += 10;
                }
                if (WillIgetThreatened(move, piece))
                {
                    RemoveSelfFromValue(valuedMove, piece);
                }
                if (WillIBeProtected(move,piece)) 
                {
                    valuedMove.value += piece.Value;
                }
                //if (CanIThreatenTheKing(move, piece))  /// Doesn't work
                //{
                //    if (!WillIgetThreatened(move, piece) || WillIBeProtected(move, piece))
                //        valuedMove.value += 25;
                //}


                GiveRandomValueToAMove(valuedMove);

                PawnMoveToPromotion(piece, valuedMove);

                valuedListOfMoves.Add(valuedMove);
               
            }                                     

            piece.ListOfMoves = valuedListOfMoves;
        }
    

    /// <summary>
    /// Checks if current move can take something
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Gives  value to move depending on what piece  it is going to take next
    /// </summary>
    /// <param name="move"></param>
    private void GiveTakeValue(Move move, Pieces piece)
    {
        double enemyValue = GetThretnedEnemyPiece(move).Value;
        bool protect = false;
        var tempenemy = GetThretnedEnemyPiece(move);

        foreach (var pmove in tempenemy.ListOfMoves )
        {
            if (AmIProtected(move, tempenemy.PieceColor) > 0)
            {
                protect = true;
            }
        }

        move.value += enemyValue;
        if (protect)
        {
            move.value -= piece.Value;
        }
    }

    private void RemoveSelfFromValue(Move move, Pieces piece)
    {
        move.value -= piece.Value;
    }

   











}
}










