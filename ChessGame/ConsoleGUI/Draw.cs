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
            Console.CursorVisible = false;

            int startPosX = 8;
            int startPosY = 8;

            foreach(var piece in game.GetGameBoard())
            {
                int posXtoPrint = startPosX - piece.CurrentPosition._PosX;
                int posYtoPrint = startPosY - piece.CurrentPosition._PosY;

                Console.SetCursorPosition(posXtoPrint, posYtoPrint);
                if ((posXtoPrint % 2) != 0 && (posYtoPrint % 2) != 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else if ((posXtoPrint % 2) != 1 && (posYtoPrint % 2) != 0)
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
                var charToPrint = (char)piece.PieceType;
                Console.Write(charToPrint);
            }

        }


    }
}
