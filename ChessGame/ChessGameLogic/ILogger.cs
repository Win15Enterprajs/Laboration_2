using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    interface ILogger
    {
        void LogPieceToMove(Pieces pieceToMove);

        void LogKilledPieceToRemove(Pieces killedPiece);

        void LogTurn();


    }
}
