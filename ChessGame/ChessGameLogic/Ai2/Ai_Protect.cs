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
            var tempPiece = GetPieceFromTempBoard(piece);
            tempPiece.CurrentPosition = move.endPositions;

            if (AmIProtected(tempPiece) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // kollar om movet kommer hota en annan pjäs

        // är jag skyddad här?!
        private int AmIProtected(Pieces piece)
        {

            int ProtectCount = 0;
            foreach (Pieces allie in Allies)
            {
                foreach (Move Amove in allie.ListOfMoves)
                {
                    if (Amove.endPositions == piece.CurrentPosition)
                        ProtectCount++;
                }
            }
            return ProtectCount;
        }



    }


}
