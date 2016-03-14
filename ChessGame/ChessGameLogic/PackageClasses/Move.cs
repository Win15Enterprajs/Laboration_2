using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic.Packages
{
    public class Move
    {
        public Point endPositions { get; set; }
        public double value { get; set; }

        public Move(Point parameterPoint, double parameterValue)
        {
            endPositions = parameterPoint;
            this.value = parameterValue;
        }

        public Move(int x, int y, double parameterPoint)
        {
            endPositions = new Point(x, y);
            value = parameterPoint;
        }

    }
}
