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
        private void Encounter(Point position, List<moves> moves, List<Board> gameplan)
        {
            int x = 0;
            int y = 0;
            foreach (var Point in moves)
            {
                if (moves.Point == Occupied)
                {
                    x = position._PosX - moves.point.x;
                    y = position._PosY - moves.point.y;
                    foreach (var item in collection)
                    {
                        if (x == 0 && y > 0)
                        {

                        }
                    }
                        
                }
            }
        }
        private bool Occupied()
        {

        }
        private void ValidMoves()
        {

        }


    }
}
