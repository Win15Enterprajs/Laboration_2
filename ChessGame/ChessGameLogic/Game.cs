using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
     public class Game
    {
        List<Pieces> GameBoard;
        Player Player1;
        Player Player2;


        public Game()
        {
            GameBoard = new List<Pieces>()
            {
                new Pawn(Color.White, new Point(0,1), false),
                new Pawn("WHITE", new Point(1,1), false),
                new Pawn("WHITE", new Point(2,1), false),
                new Pawn("WHITE", new Point(3,1), false),
                new Pawn("WHITE", new Point(4,1), false),
                new Pawn("WHITE", new Point(5,1), false),
                new Pawn("WHITE", new Point(6,1), false),
                new Pawn("WHITE", new Point(7,1), false),

            };
        }
        public List<Pieces> GetGameBoard()
        {
            return GameBoard;
        }
        private void MakeTurn()
        {

        }

    }
}
