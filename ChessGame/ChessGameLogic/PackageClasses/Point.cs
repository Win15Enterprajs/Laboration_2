using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic.Packages
{
    public class Point
    {
        public int _PosX { get; set; }
        public int _PosY { get; set; }

        public Point(int x, int y)
        {
            _PosX = x;
            _PosY = y;
        }
    }
}
