using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class Pawn : Pieces
    {
        
        public Pawn(Color color, Point CurrentPosition, bool HasbeenMoved, ChessPieceSymbol type = ChessPieceSymbol.Pawn)
            : base ( CurrentPosition, color, HasbeenMoved, type)
        {
           
        }
    }
}
