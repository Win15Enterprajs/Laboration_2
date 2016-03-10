using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    class Kevinshittepåklass
    {
        private void RëmoveMovesBehindEnemy(Dictionary<Point,Pieces> gameboard, List<moves> movesThisRound, Point position )
        {
            // Creates a new X and Y co-ordinate to be able assess the direction of the attack
            int x = 0;
            int y = 0;
            // Goes through all the points in the list of moves to see if they are occupied and if to take action
            foreach (var point in movesThisRound)
            {   // checks if point is occupied
                if (point == Occupied())
                {
                    // Takes current positon X and Y and minus the positon of the opponent to assess direction of the attack
                    x = position.x - point.x;
                    y = position.y - point.y;

                    // Checks if the newfound X and Y are positive both positive (meaning the attack came from down left)
                    if (position.x < x && position.y < y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                    }
                    // Check if the newfound X and Y are both netive (Meaning the attack came from up right)
                    if (position.x > x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                    }
                    // Check if the newfound X is smaller than the old one, and Y is larger than the old one (Meaning the attack came from up left)
                    if (position.x > x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                    }
                    // Check if the newfound X is larger than the old one, and Y is  than the old one (Meaning the attack came from down left)
                    if (position.x < x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }

                    }  
                    // Check if the newfound X is larger than the old one, and Y is  than the old one (Meaning the attack came from down left)
                    if (position.x < x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                    }
                    // Check if the newfound X is larger than the old one, and Y is  than the old one (Meaning the attack came from down left)
                    if (position.x < x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                    }
                    // Check if the newfound X is larger than the old one, and Y is  than the old one (Meaning the attack came from down left)
                    if (position.x < x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                    }
                    // Check if the newfound X is larger than the old one, and Y is  than the old one (Meaning the attack came from down left)
                    if (position.x < x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                    }
                    // Check if the newfound X is larger than the old one, and Y is  than the old one (Meaning the attack came from down left)
                    if (position.x < x && position.y > y)
                    {
                        foreach (var point in movesThisRound)
                        {
                            if (point.x < x && point.y < y)
                                point.remove();
                        }
                                            }


                }
            }
        }
        private void Occupied()
        {

        }
    }
}
