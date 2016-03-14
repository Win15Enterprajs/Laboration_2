using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic.TestClasses
{
    class TestBoard
    {

        // ska bara testa en grej


        public int[,] Board { get; set; }
        List<Move> testList = new List<Move>();
        Rook testRook = new  Rook("black", new Point(4,4), false);


        private void PrintBoard()
        {

            testList = testRook.ReturnMoveList();



            foreach (Move M in testList)
            {
                for (int x = 0; x < Board.GetLength(0); x++)
                {
                    for (int y = 0; y < Board.GetLength(1); y++)
                    {
                        

                        if (M.endPositions._PosX == x && M.endPositions._PosY == y)
                        {
                            Console.WriteLine("T");
                        }
                        else
                        {
                            Console.WriteLine(" ");
                        }

                        if (x == 7)
                        {
                            Console.WriteLine( "\n");
                        }
                    }

                } 
            }
            Console.ReadKey(true);

        }
    }
}
