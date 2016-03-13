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
        public void ReturnMovementList(Pieces piece)
        {
            if (piece == Pawn)
                PawnMovement(piece);
        }
        public void PawnMovement(Pawn pawn)
        {

        }
        public void RookMovement(Pieces rook)
        {
            var tempMoveList = new List<Move>();

            tempMoveList.AddRange(AddHorizontalMove(rook));
            tempMoveList.AddRange(AddVerticalMove(rook));

            rook.ListOfMoves = tempMoveList;

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
        private List<Move> AddHorizontalMove(Pieces piece)
        {
            var positionY = piece.CurrentPosition._PosY;
            var positionX = piece.CurrentPosition._PosX;

            int direction = 1;
            List<Move> horizontalMoves = new List<Move>();

            for (int x = positionX; x >= 0; x += direction)
            {
                if (EncounterEnemy(positionY, x))
                {
                    horizontalMoves.Add(new Move(positionY, x, 0));

                    if (x >= positionX)
                    {
                        x = positionX;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (EncounterAlly(positionY, x))
                {
                    if (x >= positionX)
                    {
                        x = positionX;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (x == 7)
                {
                    horizontalMoves.Add(new Move(positionY, x, 0));
                    x = positionX;
                    direction = -1;
                }

                else
                {
                    horizontalMoves.Add(new Move(x, positionX, 0));
                }

            }
            return horizontalMoves;
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
