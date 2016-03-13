using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;
using ChessGameLogic;

namespace ChessGameLogic
{
    class TEST_rockpossiblemoves
    {
        public int PositionY = 2;
        public int PositionX = 3;
        public bool HasBeenMoved = false;
        List<Move> possibleMoves = new List<Move>();
        
      

        public bool EncounterEnemy(int y, int x)
        {
            // logik goes here
            return false;
        }
        public bool EncounterAlly(int y, int x)
        {
            //logik goes here
            return false;
        }


        /* inte det snyggaste i världen. men det går att göra bättre*/
        private void CalculatePossibleMoves()

        {
            List<Move> templist = new List<Move>(); //makes a new list to replace the "moveList" with.

            int direction = 1; // the direction the loop counts, when the loop reverses. this value will be -1

            // adds all possible moves for the rock vertically
            //the loop will reverse as it get to y == 7. and will stop when it has counted y == 0, or when there is an ally or an enemy in the way.
            for (int y = PositionY; y >= 0; y += direction)
            {
                if (EncounterEnemy(y, PositionX))
                {
                    templist.Add(new Move(PositionX, y, 0));
                    //when the loop encounters an enemy, it ads the enemies position ^, and then reverses the iteration of the loop, starting from the rocks original position,
                    //if the loop was counting with positiv values relative to the starting position.
                    if (y >= PositionY)
                    {
                        y = PositionY;
                        direction = -1;
                    }
                    else
                        break;
                }
               else if(EncounterAlly(y,PositionX))
                {
                    //when the loop encounters an ally. it reverses the itteration of the loop, starting from the rocks original position,
                    //if the loop was counting with positiv values relative to the starting position.
                    if (y >= PositionY)
                    {
                        y = PositionY;
                        direction = -1;
                    }
                    else
                        break;
                }
                // if the loop has itterated to the edge of the board, it reverses the itteration as before.
                else if (y == 7)
                {
                    templist.Add(new Move(PositionX, y, 0));
                    y = PositionY;
                    direction = -1; 
                }
                // if there is no enemy or ally, it just adds the position. 
                //and keeps going
                else
                {
                    templist.Add(new Move(PositionX, y, 0));
                }

            }


            direction = 1; // resets the direction


            // adds all possible moves for the rock Horizontally.
            //the loop will reverse as it get to x == 7. and will stop when it has counted x == 0, or when there is an ally or an enemy in the way.
            for (int x = PositionX; x >= 0; x += direction)
            {
                if (EncounterEnemy(PositionY, x))
                {
                    templist.Add(new Move(PositionY, x, 0));

                    //when the loop encounters an enemy, it ads the enemies position ^, and then reverses the iteration of the loop, starting from the rocks original position,
                    //if the loop was counting with positiv values relative to the starting position.
                    if (x >= PositionX)
                    {
                        x = PositionX;
                        direction = -1;
                    }
                    else
                        break;
                }

                //when the loop encounters an ally. it reverses the itteration of the loop, starting from the rocks original position,
                //if the loop was counting with positiv values relative to the starting position.
                else if (EncounterAlly(PositionY, x))
                {
                    if (x >= PositionX)
                    {
                        x = PositionX;
                        direction = -1;
                    }
                    else
                        break;
                }
                // if the loop has itterated to the edge of the board, it reverses the itteration as before.
                else if (x == 7)
                {
                    templist.Add(new Move(PositionY, x, 0));
                    x = PositionX;
                    direction = -1;
                }
                // if there is no enemy or ally, it just adds the position. 
                //and keeps going.
                else
                {
                    templist.Add(new Move(x, PositionX, 0));
                }

            }
            possibleMoves = templist; //replaces the old or empty list with the new list with all possible moves the rock can do this turn.
        }
    }
}
