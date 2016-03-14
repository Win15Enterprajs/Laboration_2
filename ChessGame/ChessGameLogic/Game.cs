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
        List<Pieces> GameBoard = new List<Pieces>();
        Player Player1;
        Player Player2;


        public Game()
        {

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
