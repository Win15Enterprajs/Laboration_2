﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public enum ChessPieceSymbol
    {
        Pawn = 'P',
        Horse = 'H',
        Bishop = 'B',
        Rook = 'R',
        Queen = 'Q',
        King = 'K'
    }
    public enum Color
    {
        White,
        Black
    }
    public enum GameState
    {
        Running,
        Draw,
        Check,
        Checkmate
    }
}
