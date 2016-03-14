using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class King : Pieces
    {
        public King(string color, Point CurrentPosition, ChessPieceSymbol type = ChessPieceSymbol.King )
            : base ( color, CurrentPosition, type)
        {

        }
    }
}
