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
        public List<Move> ReturnMovementList(Pieces piece)
        {
            templist.Clear();

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

            return templist;
                

        }
        private void PawnMovement(Pieces pawn)
        {
        }
        private void RookMovement(Pieces rook)
        {
        
            templist.AddRange(AddHorizontalMove(rook));
            templist.AddRange(AddVerticalMove(rook));

            rook.ListOfMoves = templist;

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
        private void AddDiagonalMove(Pieces piece)
        {

            var positionY = piece.CurrentPosition._PosY;
            var positionX = piece.CurrentPosition._PosX;

            for (int y = positionY; y >= 0; y++)
            {
                
            }


        }
        private List<Move> AddHorizontalMove(Pieces piece)
        {
            var positionY = piece.CurrentPosition._PosY;
            var positionX = piece.CurrentPosition._PosX;

            int direction = 1;
            List<Move> horizontalMoves = new List<Move>();

            for (int x = positionX+1; x >= 0; x += direction)
            {
                if (EncounterEnemy(positionY, x))
                {
                    horizontalMoves.Add(new Move(x, positionY, 0));

                    if (x >= positionX)
                    {
                        x = positionX-1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (EncounterAlly(positionY, x))
                {
                    if (x >= positionX)
                    {
                        x = positionX-1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (x == 7)
                {
                    horizontalMoves.Add(new Move(x, positionY, 0));
                    x = positionX-1;
                    direction = -1;
                }

                else
                {
                    horizontalMoves.Add(new Move(x, positionY, 0));
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

            for (int y = positionY+1; y >= 0; y += direction)
            {
                if (EncounterEnemy(y, positionX))
                {
                    verticalMoves.Add(new Move(positionX, y, 0));

                    if (y >= positionY)
                    {
                        y = positionY-1;
                        direction = -1;
                    }
                    else
                        break;
                }
                else if (EncounterAlly(y, positionX))
                {

                    if (y >= positionY)
                    {
                        y = positionY-1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (y == 7)
                {
                    verticalMoves.Add(new Move(positionX, y, 0));
                    y = positionY-1;
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
