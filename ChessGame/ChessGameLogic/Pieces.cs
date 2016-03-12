using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    abstract class Pieces
    {
        public double Value { get; set; }
        public string Color { get; set; }
        public Point CurrentPosition { get; set; }
        public List<Move> ListOfMoves;

        public Pieces(string color, Point CurrentPosition)
        {
            this.Color = color;
            this.CurrentPosition = CurrentPosition;
        }

        public List<Move> ReturnMoveList()
        {
            return ListOfMoves;
        }
    }
}