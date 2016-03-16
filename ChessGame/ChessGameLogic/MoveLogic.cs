using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    internal class MoveLogic
    {
        List<Pieces> SnapShotOfGameboard;
        private List<Move> templist = new List<Move>();

        public void SetMovementList(Pieces piece, List<Pieces> gameBoard)
        {
            SnapShotOfGameboard = gameBoard;
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

            for (int i = 0; i < templist.Count; i++)
            {
                piece.ListOfMoves[i] = templist[i];
            }

            //kan också göra så här.
            //piece.ListOfMoves = new List<Move>(templist);

        }
        public void ClearMovementList(List<Pieces> gameboard)
        {
            foreach (var piece in gameboard)
            {
                piece.ListOfMoves.Clear();
            }
        }
        private void PawnMovement(Pieces pawn)
        {
            var positionX = pawn.CurrentPosition._PosX;
            var positionY = pawn.CurrentPosition._PosY;
            bool noEncounterOnFirstMove = false;

            List<Move> pawnMoveList = new List<Move>(); // Move = Xcoord, Ycoord and for now default value of 0.

            int direction = 1;

            if (pawn.PieceColor == Color.Black)
            {
                direction = -1;
            }

            if ((positionY + direction) < 7 && (positionY + direction) > 0) // The pawns movement along the Y-axis
            {
                if (!EncounterAlly(positionX, positionY + direction) && !EncounterEnemy(positionX, positionY + direction)) // Pawns movement along the Y-axis if it haven't moved before.
                {
                    pawnMoveList.Add(new Move(positionX, (positionY + direction), 0));
                    noEncounterOnFirstMove = true;
                }
            }
            if ((positionX - 1) >= 0 && EncounterEnemy((positionX - 1), (positionY + direction))) // Pawns AttackDirection along the X-axis.
            {
                pawnMoveList.Add(new Move((positionX - 1), (positionY + direction), 0));
            }
            if ((positionX + 1) <= 7 && EncounterEnemy((positionX - 1), (positionY + direction))) // Pawns AttackDirection along the X-axis.
            {
                pawnMoveList.Add(new Move((positionX + 1), (positionY + direction), 0));
            }


            if (pawn.hasBeenMoved == false && noEncounterOnFirstMove) // Pawns movement along the Y-axis if it haven't moved before.
            {
                if (!EncounterAlly(positionX, (positionY + direction + direction)) && !EncounterEnemy(positionX, (positionY + direction + direction))) // Checks if there is an ally or enemy in the path.
                {
                    pawnMoveList.Add(new Move(positionX, (positionY + direction + direction), 0));
                    pawn.hasBeenMoved = true;
                }
            }

            foreach (var move in pawnMoveList)
            {
                templist.Add(move);
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

            templist.AddRange(AddDiagonalMove_zero_zero(queen));
            templist.AddRange(AddDiagonalMove_zero_seven(queen));

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

            templist.AddRange(AddDiagonalMove_zero_zero(bishop));
            templist.AddRange(AddDiagonalMove_zero_seven(bishop));

            bishop.ListOfMoves = templist;
        }


        private List<Move> AddDiagonalMove_zero_zero(Pieces piece)
        {
            List<Move> diagonalMoves = new List<Move>();


            var posX = piece.CurrentPosition._PosX;
            var posY = piece.CurrentPosition._PosY;

            var x = posX;
            var y = posY;
            var direction = 1;

            do
            {
                x += direction;
                y += direction;

                if (EncounterAlly(x, y))
                {
                    if (x >= posX)
                    {
                        direction = -1;
                        x = posX;
                        y = posY;
                    }
                    else
                        break;
                }

                else if (EncounterAlly(x, y))
                {

                    diagonalMoves.Add(new Move(x, y, 0));
                    if (x >= posX || y >= posY)
                    {
                        direction = -1;
                        x = posX;
                        y = posY;
                    }
                    else
                        break;
                }

                else if (x == 7 || y == 7)
                {
                    diagonalMoves.Add(new Move(x, y, 0));
                    direction = -1;
                    x = posX;
                    y = posY;
                }

            } while (x != 0 || y != 0);

            return diagonalMoves;
        }

        private List<Move> AddDiagonalMove_zero_seven(Pieces piece)
        {
            List<Move> diagonalMoves = new List<Move>();


            var posX = piece.CurrentPosition._PosX;
            var posY = piece.CurrentPosition._PosY;

            var x = posX;
            var y = posY;
            var direction = 1;

            do
            {
                x += direction;
                y -= direction;

                if (EncounterAlly(x, y))
                {
                    if (x >= posX || y <= posY)
                    {
                        direction = -1;
                        x = posX;
                        y = posY;
                    }
                    else
                        break;
                }

                else if (EncounterAlly(x, y))
                {

                    diagonalMoves.Add(new Move(x, y, 0));
                    if (x >= posX || y <= posY)
                    {
                        direction = -1;
                        x = posX;
                        y = posY;
                    }
                    else
                        break;
                }

                else if (x == 7 || y == 0)
                {
                    diagonalMoves.Add(new Move(x, y, 0));
                    direction = -1;
                    x = posX;
                    y = posY;
                }

            } while (x != 0 || y != 7);

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
                        x = positionX - 1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (EncounterAlly(x, positionY))
                {
                    if (x >= positionX)
                    {
                        x = positionX - 1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (x == 7)
                {
                    horizontalMoves.Add(new Move(x, positionY, 0));
                    x = positionX - 1;
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

            for (int y = positionY + 1; y >= 0; y += direction)
            {
                if (EncounterEnemy(positionX, y))
                {
                    verticalMoves.Add(new Move(positionX, y, 0));

                    if (y >= positionY)
                    {
                        y = positionY - 1;
                        direction = -1;
                    }
                    else
                        break;
                }
                else if (EncounterAlly(positionX, y))
                {

                    if (y >= positionY)
                    {
                        y = positionY - 1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (y == 7)
                {
                    verticalMoves.Add(new Move(positionX, y, 0));
                    y = positionY - 1;
                    direction = -1;
                }

                else
                {
                    verticalMoves.Add(new Move(positionX, y, 0));
                }

            }
            return verticalMoves;
        }

        private bool EncounterEnemy(int x, int y,Pieces piece)
        {
            foreach (var item in SnapShotOfGameboard)
            {
                if (item.CurrentPosition._PosX == x && item.CurrentPosition._PosY == y)
                    if (item.PieceColor != piece.PieceColor)
                        return true;
            }
            return false;
        }
        private bool EncounterAlly2(int x, int y, Pieces piece)
        {
            foreach (var item in SnapShotOfGameboard)
            {
                if (item.CurrentPosition._PosX == x && item.CurrentPosition._PosY == y)
                    if (item.PieceColor == piece.PieceColor)
                        return true;
            }
            return false;
        }
        

    }
}
