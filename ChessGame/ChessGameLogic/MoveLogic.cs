﻿using System;
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
            var tempMoveList = new List<Move>();

            tempMoveList.AddRange(AddHorizontalMove(rook));
            tempMoveList.AddRange(AddVerticalMove(rook));

            rook.ListOfMoves = tempMoveList;

        }
        private void QueenMovement(Pieces queen)
        {

        }
        private void KingMovement(Pieces king)
        {

        }
        private void HorseMovement(Pieces horse)
        {
            List<Move> temporaryMoveList = new List<Move>();
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX + 1, y + 2), 0);
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX - 1, y + 2), 0);
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX + 2, y + 1), 0);
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX + 2, y - 2), 0);
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX + 1, y - 2), 0);
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX - 1, y - 2), 0);
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX - 2, y - 1), 0);
            temporaryMoveList.Add(new Move(horse.CurrentPosition._PosX - 2, y + 1), 0);

            foreach (var item in temporaryMoveList)
            {
                if (item.endPositions._PosX > 7 || item.endPositions._PosX < 0)
                    temporaryMoveList.Remove(item);
                else if (item.endPositions._PosY > 7 || item.endPositions._PosY < 0)
                    temporaryMoveList.Remove(item);
                else if (EncounterAlly(item.endPositions._PosX,item.endPositions._PosY))
                    temporaryMoveList.Remove(item);
            }
            foreach (var item in temporaryMoveList)
            {
                templist.Add(item);
            }
        }
        private void BishopMovement(Pieces bishop)
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