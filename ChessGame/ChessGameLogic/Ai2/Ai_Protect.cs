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
        // kollar om ett move är skyddat av en annan allie.
        private bool WillIBeProtected(Move move, Pieces piece)
        {
           TempGameBoard.Remove(GetPieceFromTempBoard(piece));
            

            if (AmIProtected(move, piece.PieceColor) > 0)
            {
                RestoreTempGameBoard();
                return true;
            }
            else
            {
                RestoreTempGameBoard();
                return false;
            }
        }


        // kollar om movet kommer hota en annan pjäs

        // är jag skyddad här?!
        private int AmIProtected(Move move, Color color)
        {

            int ProtectCount = 0;
            foreach (Pieces allie in TempGameBoard)
            {
                if (allie.PieceColor == color)
                {

                    foreach (Move Amove in allie.ListOfMoves)
                    {
                        if (Amove.endPositions == move.endPositions)
                            ProtectCount++;
                    }
                }
            
}
            return ProtectCount;
        }



    }


}
