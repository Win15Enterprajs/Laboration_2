using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class Queen : Pieces
    {
        public Queen(Color color, Point CurrentPosition, ChessPieceSymbol type = ChessPieceSymbol.Queen) 
            : base ( color, CurrentPosition,type)
        {

        }
    }
}
