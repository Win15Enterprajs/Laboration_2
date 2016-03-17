using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class AI
    {
        // templista för att kunna skriva AI
        List<Pieces> tempgameboard = new List<Pieces>();
       
        public /*List<Move>*/ void GiveValuetoMoves(Pieces piece)
        {          
            foreach (var item in piece.ListOfMoves)
            {
                if (CanThisMoveTakeSomething(item))
                {
                    GiveInitialTakeValue(item);
                    RemoveSelfFromValue(item, piece);
                }
                else
                    GiveRandomValueToAMove(item);
               
            }
           
        }
        private void GiveInitialTakeValue(Move move)
        {
                foreach (var opponent in tempgameboard)
                {

                        if (opponent.PieceType == ChessPieceSymbol.Bishop)
                            move.value = 30;
                        if (opponent.PieceType == ChessPieceSymbol.Horse)
                            move.value = 30;
                        if (opponent.PieceType == ChessPieceSymbol.Pawn)
                            move.value = 30;
                        if (opponent.PieceType == ChessPieceSymbol.Queen)
                            move.value = 90;
                        if (opponent.PieceType == ChessPieceSymbol.Rook)
                            move.value = 50;

                }
                
        }
        private Move RemoveSelfFromValue(Move move, Pieces piece)
        {
            if (piece.PieceType == ChessPieceSymbol.Bishop)
                move.value = move.value - (30 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Horse)
                move.value = move.value - (30 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Queen)
                move.value = move.value - (90 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Rook)
                move.value = move.value - (30 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Pawn)
                    move.value = move.value - (10 * 0.5);
            return move;
            
        }
        private bool CanThisMoveTakeSomething(Move move)
        {
            foreach (var item in tempgameboard)
            {
                if (item.CurrentPosition == move.endPositions)
                    return true;
            }
            return false;
        }
        private void GiveRandomValueToAMove(Move move)
        {
            Random rnd = new Random();
            int nr = rnd.Next(0, 10);
            move.value = nr;
        }
       
    }
}
