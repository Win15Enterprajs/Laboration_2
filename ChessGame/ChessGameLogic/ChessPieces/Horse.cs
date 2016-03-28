using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class Horse : Pieces
    {
        
        public Horse(Color color, Point CurrentPosition, ChessPieceSymbol type = ChessPieceSymbol.Horse): base ( color, CurrentPosition, type)
        {
            this.Value = 30;
        }
    }
}
