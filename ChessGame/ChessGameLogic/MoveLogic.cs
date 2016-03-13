using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    class MoveLogic
    {
        public void ReturnMovementList(Pieces piece)
        {
            if (piece is Pawn)
                PawnMovement(piece);
        }
        public void PawnMovement(Pieces pawn)
        {
        }
        public void RookMovement()
        {

        }
        public void QueenMovement()
        {

        }
        public void KingMovement()
        {

        }
        public void HorseMovement()
        {

        }
        public void BishopMovement()
        {

        }
        private void AddDiagonalMove()
        {

        }
        private void AddHorizontalMove()
        {

        }
        private void AddVerticalMove()
        {

        }
    }
}
