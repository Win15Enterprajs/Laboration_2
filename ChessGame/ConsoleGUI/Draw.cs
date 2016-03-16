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
            printBoard();

            int startPosX = 0;
            int startPosY = 8;

            int stepover = 3;

            foreach(var piece in game.GetGameBoard())
            {
                int posXtoPrint = (startPosX + piece.CurrentPosition._PosX);
                int posYtoPrint = (startPosY - piece.CurrentPosition._PosY);

                Console.SetCursorPosition((posXtoPrint * 3 ) + 4, posYtoPrint);


                if ((posXtoPrint % 2) != 0 && (posYtoPrint % 2) != 0 || (posXtoPrint % 2) != 1 && (posYtoPrint % 2) != 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else if ((posXtoPrint % 2) != 1 && (posYtoPrint % 2) != 0 || (posXtoPrint % 2) != 0 && (posYtoPrint % 2) != 1)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                if (piece.PieceColor == Color.Black)
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

        private void printBoard()
        {
            for (int i = 3; i < 25; i +=3)
            {
                for (int j = 1; j < 9; j++)
                {
                    Console.SetCursorPosition(i, j);
                    if ((i % 2) != 1 && (j % 2) != 0 || (i % 2) != 0 && (j % 2) != 1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    else if ((i % 2) != 0 && (j % 2) != 0 || (i % 2) != 1 && (j % 2) != 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    Console.Write("   ");
                }
            }
   
        }

        //(i % 2) != 0 && (j % 2) != 0 || (i % 2) != 1 && (j % 2) != 1
    }
}
