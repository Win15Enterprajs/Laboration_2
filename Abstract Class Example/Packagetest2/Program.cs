using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packagetest2
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Vi skapar upp två points. dvs. koordinater.
            Vi skapar upp två pjäser, Bishop och Pawn som är subklasser av Piece.

            Vi lägger till våra pjäser i våran dictionary där våra Point är Key och våran pjäs är Value.

            */
            
            var point = new Point(0,4);   
            var point2 = new Point(1, 3);
            var piece = new Pawn("Pawn");

            var dict = new Dictionary<Point, Piece>();
            var piece2 = new Bishop("Bishop");

            dict.Add(point, piece);  
            dict.Add(point2, piece2);


            foreach (var item in dict)     // Vi loopar igenom våran dictionary  och kallar på den virtuella metoden som vi overridat i Pawn och Bishop.
            {
                item.Value.calculateMove();
            }

            Console.ReadKey();

        }
    }
}
