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
        Dictionary<int, char> CoordinateConversionToLetter;

        public Draw()
        {
            CoordinateConversionToLetter = new Dictionary<int, char>()
            {
                [0] = 'A',
                [1] = 'B',
                [2] = 'C',
                [3] = 'D',
                [4] = 'E',
                [5] = 'F',
                [6] = 'G',
                [7] = 'H',

            };
        }


        public void PrintGameBoard(Game game)
        {
            Console.CursorVisible = false;
            PrintBoard();

            int startPosX = 0;
            int startPosY = 8;


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
            Console.BackgroundColor = ConsoleColor.Black;

        }

        private void PrintBoard()
        {
            for (int i = 3; i < 25; i +=3)
            {
               
                for (int j = 1; j < 9; j++)
                {
                    Console.SetCursorPosition(1, j);
                    Console.BackgroundColor = ConsoleColor.Black;
                   Console.Write(j);

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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            int count = 0;
            for (int i = 4; i < 26; i += 3)
            {

                Console.SetCursorPosition(i, 9);
                Console.Write(CoordinateConversionToLetter[count]);
                Console.SetCursorPosition(i, 0);
                Console.Write(CoordinateConversionToLetter[count]);
                count++;
            }

            count = 8;
            for (int i = 1; i < 9; i++)
            {

                Console.SetCursorPosition(1, i );
                Console.Write(count);
                Console.SetCursorPosition(28, i );
                Console.Write(count);
                count--;
            }


        }

        public void PrintGameLog(Game game)
        {
            string[] LoggToPrint = new string[game.getGameLog().Count];
            game.getGameLog().CopyTo(LoggToPrint);
            int cursorX = 33;
            int cursorY = 8;
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = LoggToPrint.Length - 1; i >= 0; i--)
            {
                Console.SetCursorPosition(cursorX, cursorY);
                if (LoggToPrint.Length > 0)
                {
                    Console.WriteLine(LoggToPrint[i]); 
                }
                cursorY--; 
                if(cursorY < 1)
                {
                    break;
                }
            }
            
            
        }
        public void PrintResult(Game Chess)
        {
            while (true)
            {
                Console.WriteLine($"{ Chess.State}");
                Console.ReadKey();
            }

        }
    }
}
