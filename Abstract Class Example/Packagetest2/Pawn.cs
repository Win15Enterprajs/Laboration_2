using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packagetest2
{
    class Pawn : Piece
    {
        public Pawn(string type): base (type)
        {
            this.PieceType = type;
        }

        public override void calculateMove() // Här overridar vi den virtuella metoden.
        {
            Console.WriteLine("Pawn"); // Här skriver vi ut pawn
            base.Derpa(); // här kallar vi på en privat metod i basklassen
            ost(); // här kallar vi på en privat metod ifrån denna klassen.
        }

        private void ost()
        {
            Console.WriteLine("Hello!");
        }
    }
}
