using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    class MoveLogic
    {
        private List<Move> templist = new List<Move>();
        public void ReturnMovementList(Pieces piece)
        {
            if (piece is Pawn)
                PawnMovement(piece);
            if (piece is Rook)
                RookMovement(piece);
            if (piece is Queen)
                QueenMovement(piece);
            if (piece is King)
                KingMovement(piece);
            if (piece is Horse)
                HorseMovement(piece);
            if (piece is Bishop)
                BishopMovement(piece);

        }
        public void PawnMovement(Pieces pawn)
        {

        }
        public void RookMovement(Pieces rook)
        {

        }
        public void QueenMovement(Pieces queen)
        {

        }
        public void KingMovement(Pieces king)
        {

        }
        public void HorseMovement(Pieces horse)
        {

        }
        public void BishopMovement(Pieces bishop)
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
