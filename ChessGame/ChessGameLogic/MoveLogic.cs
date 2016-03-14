﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    internal class MoveLogic
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
            var positionX = pawn.CurrentPosition._PosX;
            var positionY = pawn.CurrentPosition._PosY;
            List<Point> possibleMoves = new List<Point>();

            int direction = 1;

            if (pawn.Color == "BLACK")
            {
                direction = -1;  
            }

            if ((positionY + direction) < 7 && (positionY + direction) > 0)
            {
                possibleMoves.Add(new Point(positionX, (positionY + direction)));
            }
            if ((positionX - 1) >= 0 && EncounterEnemy((positionX - 1), (positionY + direction)))
            {
                possibleMoves.Add(new Point((positionX - 1), (positionY + direction)));
            }
            if ((positionX + 1) <= 7 && EncounterEnemy((positionX - 1), (positionY + direction)))
            {
                possibleMoves.Add(new Point((positionX + 1), (positionY + direction)));
            }

            if (pawn.hasBeenMoved == false)
            {
                possibleMoves.Add(new Point((positionX - 1), (positionY + direction + 1)));
            }


            foreach(var possibleMove in possibleMoves)  /// ev. integrera detta i if satserna ovanför.
            {
                int posX = possibleMove._PosX;
                int posY = possibleMove._PosY;

                if(positionX != posX && EncounterEnemy(posX, posY))
                {
                    templist.Add(new Move(possibleMove, 0));
                }
                if(positionX == possibleMove._PosX && !EncounterAlly(posX, posY))
                {
                    templist.Add(new Move(possibleMove, 0));
                }
            }




        }

        private void RookMovement(Pieces rook)
        {
            templist.AddRange(AddHorizontalMove(rook));
            templist.AddRange(AddVerticalMove(rook));

            rook.ListOfMoves = templist;
        }

        private void QueenMovement(Pieces queen)
        {
            templist.AddRange(AddHorizontalMove(queen));
            templist.AddRange(AddVerticalMove(queen));
            templist.AddRange(AddDiagonalMove(queen));

            queen.ListOfMoves = templist;
        }

        private void KingMovement(Pieces king)
        {
            var x = king.CurrentPosition._PosX;
            var y = king.CurrentPosition._PosY;
            List<Move> kingMoveList = new List<Move>();
            kingMoveList.Add(new Move((x), (y + 1), 0));
            kingMoveList.Add(new Move((x + 1), (y + 1), 0));
            kingMoveList.Add(new Move((x + 1), (y), 0));
            kingMoveList.Add(new Move((x + 1), (y - 1), 0));
            kingMoveList.Add(new Move((x - 1), (y - 1), 0));
            kingMoveList.Add(new Move((x), (y - 1), 0));
            kingMoveList.Add(new Move((x - 1), (y - 1), 0));
            kingMoveList.Add(new Move((x - 1), (y), 0));
            kingMoveList.Add(new Move((x - 1), (y + 1), 0));

            foreach (var item in kingMoveList)
            {
                if (item.endPositions._PosX > 7 || item.endPositions._PosX < 0)
                    kingMoveList.Remove(item);
                else if (item.endPositions._PosY > 7 || item.endPositions._PosY < 0)
                    kingMoveList.Remove(item);
                else if (EncounterAlly(item.endPositions._PosX, item.endPositions._PosY))
                    kingMoveList.Remove(item);
            }
            foreach (var item in kingMoveList)
            {
                templist.Add(item);
            }
        }

        private void HorseMovement(Pieces horse)
        {
            var x = horse.CurrentPosition._PosX;
            var y = horse.CurrentPosition._PosY;
            List<Move> horseMoveList = new List<Move>();
            horseMoveList.Add(new Move((x + 1), (y + 2), 0));
            horseMoveList.Add(new Move((x - 1), (y + 2), 0));
            horseMoveList.Add(new Move((x + 2), (y + 1), 0));
            horseMoveList.Add(new Move((x + 2), (y - 2), 0));
            horseMoveList.Add(new Move((x + 1), (y - 2), 0));
            horseMoveList.Add(new Move((x - 1), (y - 2), 0));
            horseMoveList.Add(new Move((x - 2), (y - 1), 0));
            horseMoveList.Add(new Move((x - 2), (y + 1), 0));

            foreach (var item in horseMoveList)
            {
                if (item.endPositions._PosX > 7 || item.endPositions._PosX < 0)
                    horseMoveList.Remove(item);
                else if (item.endPositions._PosY > 7 || item.endPositions._PosY < 0)
                    horseMoveList.Remove(item);
                else if (EncounterAlly(item.endPositions._PosX, item.endPositions._PosY))
                    horseMoveList.Remove(item);
            }
            foreach (var item in horseMoveList)
            {
                templist.Add(item);
            }
        }

        private void BishopMovement(Pieces bishop)
        {

        }

        private List<Move> AddDiagonalMove_zero_zero(Pieces piece)
        {
            var positionY = piece.CurrentPosition._PosY;
            var positionX = piece.CurrentPosition._PosX;

            var posx = positionX+1;
            var posy = positionY;


            List<Move> diagonalMoves = new List<Move>();

            var direction = 1;
            var end = 7;


            while (posx != 0 || posy != 0)
            {

                for (int y = positionY + direction; y >= end; y += direction)

                {
                    if (EncounterEnemy(posx, y))
                    {
                        diagonalMoves.Add(new Move(posx, y, 0));

                        if (y >= positionY)
                        {
                            y = positionY - 1;
                            posx = positionX - 1;
                            direction = -1;
                        }
                        else
                            break;
                    }

                    else if (EncounterAlly(posx, y))
                    {
                        if (y >= positionY)
                        {
                            y = positionY - 1;
                            posx = positionX - 1;
                            direction = -1;
                        }
                        else
                            break;
                    }

                    else if (y == 7 || posx == 7)
                    {
                        diagonalMoves.Add(new Move(posx, y, 0));
                        y = positionY - 1;
                        posx = positionX - 1;
                        direction = -1;
                        end = 0;
                    }

                    else
                    {
                        diagonalMoves.Add(new Move(posx, positionX, 0));
                    }
                    posx += direction;
                    posy = y;
                }
                


            }
            return diagonalMoves;
        }
        private List<Move> AddHorizontalMove(Pieces piece)
        {
            var positionY = piece.CurrentPosition._PosY;
            var positionX = piece.CurrentPosition._PosX;

            int direction = 1;
            List<Move> horizontalMoves = new List<Move>();

            for (int x = positionX; x >= 0; x += direction)
            {
                if (EncounterEnemy(x, positionY))
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

                else if (EncounterAlly(x, positionY))
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

            for (int y = positionY+1; y >= 0; y += direction)
            {
                if (EncounterEnemy(positionX, y))
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
                else if (EncounterAlly(positionX, y))
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
