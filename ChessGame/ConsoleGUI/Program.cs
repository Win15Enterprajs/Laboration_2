using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic;

namespace ConsoleGUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            Game Chess = new Game();
            Draw DrawStuff = new Draw();
            do
            {
                Console.Clear();
                DrawStuff.PrintGameBoard(Chess);
                DrawStuff.printGameLog(Chess);
                Console.ReadKey();
                Chess.ThisIsIt();
            } while (true);
        }
    }
}
