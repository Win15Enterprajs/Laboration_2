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
        public Pawn(string color, Point CurrentPosition, bool HasbeenMoved): base ( CurrentPosition, color, HasbeenMoved)
        {

        }
    }
}
