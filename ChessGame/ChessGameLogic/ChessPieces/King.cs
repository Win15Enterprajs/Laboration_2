﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class King : Pieces
    {

        public King(Color color, Point CurrentPosition, ChessPieceSymbol type = ChessPieceSymbol.King )
            : base ( color, CurrentPosition, type)
        {
            this.Value = 0;
        }
    }
}
