using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic;

namespace ChessGameLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            Game Chess = new Game();
            Draw DrawStuff = new Draw();            
            DrawStuff.PrintGameBoard(Chess);
        }
    }
}
