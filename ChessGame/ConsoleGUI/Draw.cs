using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic;

namespace ChessGameLogic
{
    class Draw
    {
        public void PrintGameBoard(Game game)
        {
            int startPosX = 1;
            int startPosY = 8;

            foreach(var piece in game.GetGameBoard())
            {
                int posXtoPrint = startPosX - piece.CurrentPosition._PosX;
                int posYtoPrint = startPosY - piece.CurrentPosition._PosY;

                Console.SetCursorPosition(posYtoPrint, posXtoPrint);
                if ((posXtoPrint % 2) == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                if (piece.Color == "BLACK")
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(piece.PieceType.ToString());
            }

        }


    }
}
