using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packagetest2
{
    abstract class Piece
    {
        public string PieceType { get; set; }

        public Piece(string type)
        {
            this.PieceType = type;
        }

        public virtual void calculateMove()
        {
            Console.WriteLine("Herpaderp");
        }

        protected void Derpa() // Protected så att pjäserna kan komma åt denna metoden.
        {
            Console.WriteLine("Derpa");
        }
    }
}
