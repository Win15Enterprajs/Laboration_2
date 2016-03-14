﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    class Rook : Pieces
    {
        public Rook(string color, Point CurrentPosition, bool hasBeenMoved, ChessPieceSymbol type = ChessPieceSymbol.Rook) 
            : base ( color, CurrentPosition, type)
        {

        }
    }
}
