using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packagetest2
{
    class Point
    {
        public int positionX { get; set; }
        public int positionY { get; set; }
        public Point(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
        }
    }
}
