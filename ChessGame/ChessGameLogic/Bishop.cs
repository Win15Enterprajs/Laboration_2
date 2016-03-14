﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class Bishop : Pieces
    {
        public Bishop(string color, Point CurrentPosition, ChessPieceSymbol type = ChessPieceSymbol.Bishop) 
            : base ( color, CurrentPosition, type)
        {
            
        }

    }
}
