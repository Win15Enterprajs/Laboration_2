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
        public List<Move> GiveValuetoMoves(List<Move> movelist, Pieces piece)
        {

        }
        private void GiveInitialTakeValue(List<Move> movelist, Pieces piece)
        {
            foreach (var item in movelist)
            {
                foreach (var opponent in tempgameboard)
                {
                    if (item.endPositions == opponent.CurrentPosition)
                    {
                        if (opponent.PieceType == ChessPieceSymbol.Bishop)
                            item.value = 3;
                        if (opponent.PieceType == ChessPieceSymbol.Horse)
                            item.value = 3;
                        if (opponent.PieceType == ChessPieceSymbol.Pawn)
                            item.value = 3;
                        if (opponent.PieceType == ChessPieceSymbol.Queen)
                            item.value = 9;
                        if (opponent.PieceType == ChessPieceSymbol.Rook)
                            item.value = 5;
                    }
                }
                
            }
        }
        private Move RemoveSelfFromValue(Move move, Pieces piece)
        {
            if (piece.PieceType == ChessPieceSymbol.Bishop)
                move.value = move.value - (3 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Horse)
                move.value = move.value - (3 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Queen)
                move.value = move.value - (9 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Rook)
                move.value = move.value - (3 * 0.5);
            if (piece.PieceType == ChessPieceSymbol.Pawn)
                    move.value = move.value - (1 * 0.5);
            return move;
            
        }
    }
}
