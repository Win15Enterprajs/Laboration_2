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
                            item.value = 5;
                    }
                }
                
            }
        }
    }
}
