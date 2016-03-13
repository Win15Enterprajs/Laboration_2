using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

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
        private List<Move> AddVerticalMove(Pieces piece)
        {
            List<Move> verticalMoves = new List<Move>();

            var positionY = piece.CurrentPosition._PosY;
            var positionX = piece.CurrentPosition._PosX;

            int direction = 1;

            for (int y = positionY; y >= 0; y += direction)
            {
                if (EncounterEnemy(y, positionX))
                {
                    verticalMoves.Add(new Move(positionX, y, 0));

                    if (y >= positionY)
                    {
                        y = positionY;
                        direction = -1;
                    }
                    else
                        break;
                }
                else if (EncounterAlly(y, positionX))
                {

                    if (y >= positionY)
                    {
                        y = positionY;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (y == 7)
                {
                    verticalMoves.Add(new Move(positionX, y, 0));
                    y = positionY;
                    direction = -1;
                }

                else
                {
                    verticalMoves.Add(new Move(positionX, y, 0));
                }

            }
            return verticalMoves;
        }

        public bool EncounterEnemy(int y, int x)
        {
            // logik goes here
            return false;
        }
        public bool EncounterAlly(int y, int x)
        {
            //logik goes here
            return false;
        }
    }
}
