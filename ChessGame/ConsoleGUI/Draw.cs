using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic;

namespace ConsoleGUI
{
    class Draw
    {
        public void PrintGameBoard(Game game)
        {
            int startPosX = 8;
            int startPosY = 8;

            foreach(var piece in game.GetGameBoard())
            {
                int posXtoPrint = startPosX - piece.CurrentPosition._PosX;
                int posYtoPrint = startPosY - piece.CurrentPosition._PosY;

                Console.SetCursorPosition(posYtoPrint, posXtoPrint);
                
            }

        }


    }
}
