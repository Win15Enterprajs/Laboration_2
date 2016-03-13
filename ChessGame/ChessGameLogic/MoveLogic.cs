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

            else if (piece is Rook)
                RookMovement(piece);

            else if (piece is Queen)
                QueenMovement(piece);

            else if (piece is King)
                KingMovement(piece);

            else if (piece is Horse)
                HorseMovement(piece);

            else if (piece is Bishop)
                BishopMovement(piece);
            else 
                // something went horribly wrong

        }
        private void PawnMovement(Pieces pawn)
        {

        }
        private void RookMovement(Pieces rook)
        {

        }
        private void QueenMovement(Pieces queen)
        {

        }
        private void KingMovement(Pieces king)
        {

        }
        private void HorseMovement(Pieces horse)
        {

        }
        private void BishopMovement(Pieces bishop)
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
