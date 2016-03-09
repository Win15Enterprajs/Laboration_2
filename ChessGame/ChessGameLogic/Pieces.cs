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
        public double _PieceValue { get; set; }
        public Point _CurrentPosition { get; set; }
        public Move _BestPossibleMove { get; set; }
        public List<Move> ListOfPossibleMoves;

        public Pieces(double PieceValue, Point CurrentPosition)
        {
            _PieceValue = PieceValue;
            _CurrentPosition = CurrentPosition;
        }

        public virtual void CalculateMove()
        {

        }

        private void WithinBoard()
        {

        }
        private void Encounter()
        {

        }
        private void ValidMoves()
        {

        }


    }
}
