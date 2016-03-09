using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packagetest2
{
    class Bishop : Piece
    {
        public Bishop(string type): base (type)
        {
            this.PieceType = type;
        }

        public override void calculateMove()
        {
            Console.WriteLine("Bishop");
            base.Derpa();
        }
    }
}
