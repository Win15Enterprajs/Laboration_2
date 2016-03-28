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
        /// <summary>
        /// This starts our game.
        /// We create instances of Game and Draw.
        /// The game is what play they game.
        /// 
        /// The Draw class takes an instance of game so it can look
        /// at the values and draw our gameboard and gamelog.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            Game Chess = new Game();
            Draw DrawStuff = new Draw();
            do
            {
                Console.Clear();
                DrawStuff.PrintPieces(Chess);
                DrawStuff.PrintGameLog(Chess);
                Console.ReadKey();
                Chess.PlayGame();
            } while (Chess.State == GameState.Running || Chess.State == GameState.Check); // The game will run until the gamestate is Draw or Checkmate.

            do
            {
                Console.Clear();
                DrawStuff.PrintPieces(Chess);
                DrawStuff.PrintGameLog(Chess);
                DrawStuff.PrintResult(Chess);
                Console.ReadKey();
            } while (true);
        }
    }
}
