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
            SnapShotOfGameboard = new List<Pieces>(gameBoard);
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


            piece.ListOfMoves = new List<Move>(templist);
            //for (int i = 0; i < templist.Count; i++)
            //{
            //    piece.ListOfMoves.Add(templist[i]);
            //}
            for (int i = 0; i < piece.ListOfMoves.Count; i++)
            {
                if (WillItChessYou(piece, piece.ListOfMoves[i], gameBoard))
                {
                    piece.ListOfMoves.Remove(piece.ListOfMoves[i]);
                }
                //foreach (var item in piece.ListOfMoves)
                //{
                //    if (WillItChessYou(piece, item,gameBoard))
                //        piece.ListOfMoves.Remove(item);
                //}

            }
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


            int direction = 1;

            if (pawn.PieceColor == Color.Black)
            {
                direction = -1;
            }

            List<Move> pawnMoveList = new List<Move>()
            {
                new Move(positionX - 1, positionY + direction, 0),
                new Move(positionX + 1, positionY + direction, 0),
                new Move(positionX, positionY + direction, 0),
                new Move(positionX, positionY + (direction + direction), 0)

            };
            for (int i = 0; i < pawnMoveList.Count; i++)
            {
                var posX = pawnMoveList[i].endPositions._PosX;
                var posY = pawnMoveList[i].endPositions._PosY;

                if (!EncounterAlly(posX, posY, pawn) && (!EncounterEnemy(posX, posY, pawn)))
                {
                    if ((posX <= 7) && posX >= 0 && (posX == positionX))
                    {
                        if (posY <= 7 && posY >= 0)
                        {
                            if (noEncounterOnFirstMove == false)
                            {
                                templist.Add(pawnMoveList[i]);
                                noEncounterOnFirstMove = true;
                            }
                            else if (noEncounterOnFirstMove == true && pawn.hasBeenMoved == false)
                            {
                                templist.Add(pawnMoveList[i]);
                            }
                        }
                    }
                }
                else if (EncounterEnemy(posX, posY, pawn) && posX != positionX )
                {
                    templist.Add(pawnMoveList[i]);
                }
            }


            //////////////////////

            //if ((positionY + direction) < 7 && (positionY + direction) > 0) // The pawns movement along the Y-axis
            //{
            //    if (!EncounterAlly(positionX, positionY + direction,pawn) && !EncounterEnemy(positionX, positionY + direction,pawn)) // Pawns movement along the Y-axis if it haven't moved before.
            //    {
            //        templist.Add(new Move(positionX, (positionY + direction), 0));
            //        noEncounterOnFirstMove = true;
            //    }
            //}
            //if ((positionX - 1) >= 0 && EncounterEnemy((positionX - 1), (positionY + direction),pawn)) // Pawns AttackDirection along the X-axis.
            //{
            //    templist.Add(new Move((positionX - 1), (positionY + direction), 0));
            //}
            //if ((positionX + 1) <= 7 && EncounterEnemy((positionX - 1), (positionY + direction),pawn)) // Pawns AttackDirection along the X-axis.
            //{
            //    templist.Add(new Move((positionX + 1), (positionY + direction), 0));
            //}


            //if (pawn.hasBeenMoved == false && noEncounterOnFirstMove) // Pawns movement along the Y-axis if it haven't moved before.
            //{
            //    if (!EncounterAlly(positionX, (positionY + direction + direction),pawn) && !EncounterEnemy(positionX, (positionY + direction + direction),pawn)) // Checks if there is an ally or enemy in the path.
            //    {
            //        templist.Add(new Move(positionX, (positionY + (direction + direction)), 0));
            //        pawn.hasBeenMoved = true;
            //    }
            //}

            //foreach (var move in pawnMoveList)
            //{
            //    templist.Add(move);
            //}




        }

        private void RookMovement(Pieces rook)
        {
            templist.AddRange(AddHorizontalMove(rook));
            templist.AddRange(AddVerticalMove(rook));
        }

        private void QueenMovement(Pieces queen)
        {
            templist.AddRange(AddHorizontalMove(queen));
            templist.AddRange(AddVerticalMove(queen));

            templist.AddRange(AddDiagonalMove_zero_seven(queen));
            templist.AddRange(AddDiagonalMove_zero_zero(queen));
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
            kingMoveList.Add(new Move((x - 1), (y), 0));
            kingMoveList.Add(new Move((x - 1), (y + 1), 0));


            for (int i = 0; i < kingMoveList.Count; i++)
            {
                if (!(EncounterAlly(kingMoveList[i].endPositions._PosX, kingMoveList[i].endPositions._PosY, king)))
                {
                    if (kingMoveList[i].endPositions._PosX <= 7 && kingMoveList[i].endPositions._PosX >= 0)
                    {
                        if (kingMoveList[i].endPositions._PosY <= 7 && kingMoveList[i].endPositions._PosY >= 0)
                        {
                            templist.Add(kingMoveList[i]);
                        }
                    }
                }

            }

            //for (int i = 0; i < kingMoveList.Count; i++)
            //{
            //    if (kingMoveList[i].endPositions._PosX > 7 || kingMoveList[i].endPositions._PosX < 0)
            //        kingMoveList.Remove(kingMoveList[i]);
            //    else if (kingMoveList[i].endPositions._PosY > 7 || kingMoveList[i].endPositions._PosY < 0)
            //        kingMoveList.Remove(kingMoveList[i]);
            //    else if (EncounterAlly(kingMoveList[i].endPositions._PosX, kingMoveList[i].endPositions._PosY, king))
            //        kingMoveList.Remove(kingMoveList[i]);
            //}

            //foreach (var item in kingMoveList)
            //{
            //    if (item.endPositions._PosX > 7 || item.endPositions._PosX < 0)
            //        kingMoveList.Remove(item);
            //    else if (item.endPositions._PosY > 7 || item.endPositions._PosY < 0)
            //        kingMoveList.Remove(item);
            //    else if (EncounterAlly(item.endPositions._PosX, item.endPositions._PosY,king))
            //        kingMoveList.Remove(item);
            //}
            //foreach (var item in kingMoveList)
            //{
            //    templist.Add(item);
            //}
        }

        private void HorseMovement(Pieces horse)
        {
            var x = horse.CurrentPosition._PosX;
            var y = horse.CurrentPosition._PosY;
            List<Move> horseMoveList = new List<Move>();
            horseMoveList.Add(new Move((x + 1), (y + 2), 0));
            horseMoveList.Add(new Move((x - 1), (y + 2), 0));
            horseMoveList.Add(new Move((x + 2), (y + 1), 0));
            horseMoveList.Add(new Move((x + 2), (y - 1), 0));
            horseMoveList.Add(new Move((x + 1), (y - 2), 0));
            horseMoveList.Add(new Move((x - 1), (y - 2), 0));
            horseMoveList.Add(new Move((x - 2), (y - 1), 0));
            horseMoveList.Add(new Move((x - 2), (y + 1), 0));
            //for (int i = 0; i < horseMoveList.Count; i++)
            //{
            //    if (horseMoveList[i].endPositions._PosX <= 7 || horseMoveList[i].endPositions._PosX >= 0)
            //        templist.Add(horseMoveList[i]);
            //    else if (horseMoveList[i].endPositions._PosY <= 7 || horseMoveList[i].endPositions._PosY >= 0)
            //        templist.Add(horseMoveList[i]);

            //    else if (EncounterAlly(horseMoveList[i].endPositions._PosX, horseMoveList[i].endPositions._PosY, horse))
            //        horseMoveList.Remove(horseMoveList[i]);
            //}

            for (int i = 0; i < horseMoveList.Count; i++)
            {
                if (!(EncounterAlly(horseMoveList[i].endPositions._PosX, horseMoveList[i].endPositions._PosY, horse)))
                {
                    if (horseMoveList[i].endPositions._PosX <= 7 && horseMoveList[i].endPositions._PosX >= 0)
                    {
                        if (horseMoveList[i].endPositions._PosY <= 7 && horseMoveList[i].endPositions._PosY >= 0)
                        {
                            templist.Add(horseMoveList[i]);
                        }
                    }
                }
                    
            }

            //foreach (var item in horseMoveList)
            //{
            //    if (item.endPositions._PosX > 7 || item.endPositions._PosX < 0)
            //        horseMoveList.Remove(item);
            //    else if (item.endPositions._PosY > 7 || item.endPositions._PosY < 0)
            //        horseMoveList.Remove(item);
            //    else if (EncounterAlly(item.endPositions._PosX, item.endPositions._PosY,horse))
            //        horseMoveList.Remove(item);
            //}
            //foreach (var item in horseMoveList)
            //{
            //    templist.Add(item);
            //}
        }

        private void BishopMovement(Pieces bishop)
        {

            templist.AddRange(AddDiagonalMove_zero_seven(bishop));
            templist.AddRange(AddDiagonalMove_zero_zero(bishop));
        }

        private List<Move> AddDiagonalMove(Pieces piece)
        {
            List<Move> diagonalmoves = new List<Move>();
            bool withinbounds = true;
            int x = piece.CurrentPosition._PosX;
            int y = piece.CurrentPosition._PosY;
            do
            {

                // Adding spaces up to the right
                for (int i = 0; i < 6; i++)
                {
                    if (x <= 7 && y <= 7)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            diagonalmoves.Add(new Move(x, y, 0));
                            break;
                        }
                        else if (EncounterAlly(x,y,piece))
                        {
                            break;
                        }
                        else
                        {
                            if (x != piece.CurrentPosition._PosX && y != piece.CurrentPosition._PosY)
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                            }
                            x++;
                            y++;
                        }
                    }
                    else
                        break;
                }
                // adding moves down left
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                for (int i = 0; i < 6; i++)
                {
                    if (x >= 0 && y >= 0)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            diagonalmoves.Add(new Move(x, y, 0));
                            break;
                        }
                        else if (EncounterAlly(x, y, piece))
                        {
                            break;
                        }
                        else
                        {
                            if (x != piece.CurrentPosition._PosX && y != piece.CurrentPosition._PosY)
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                            }
                            x--;
                            y--;
                        }
                    }
                    else
                        break;
                }
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                // adding moves down to the right
                for (int i = 0; i < 6; i++)
                {
                    if (x <= 7 && y >= 0)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            diagonalmoves.Add(new Move(x, y, 0));
                            break;
                        }
                        else if (EncounterAlly(x, y, piece))
                        {
                            break;
                        }
                        else
                        {
                            if (x != piece.CurrentPosition._PosX && y != piece.CurrentPosition._PosY)
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                            }
                            x++;
                            y--;
                        }
                    }
                    else
                        break;
                }
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                // adding moves up to the left
                for (int i = 0; i < 6; i++)
                {
                    if (x >= 0 && y <= 7)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            diagonalmoves.Add(new Move(x, y, 0));
                            break;
                        }
                        else if (EncounterAlly(x, y, piece))
                        {
                            break;
                        }
                        else
                        {
                            if (x != piece.CurrentPosition._PosX && y != piece.CurrentPosition._PosY)
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                            }
                            x--;
                            y++;
                        }
                    }
                    else
                        withinbounds = false;
                }
            } while (withinbounds);
            return diagonalmoves;
        }
        private List<Move> AddDiagonalMove_zero_zero(Pieces piece)
        {
            List<Move> diagonalMoves = new List<Move>();


            var posX = piece.CurrentPosition._PosX;
            var posY = piece.CurrentPosition._PosY;

            var x = posX;
            var y = posY;
            if (x == 7)
                x = 6;
            if (y == 7)
                y = 6;
             
            var direction = 1;

            do
            {
                x += direction;
                y += direction;

                if (EncounterAlly(x, y,piece))
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

                else if (EncounterEnemy(x, y,piece))
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

            } while (x >= 0 || y >= 0);
            for (int i = 0; i < diagonalMoves.Count; i++)
            {
                
            }
            return diagonalMoves;
        }

        private List<Move> AddDiagonalMove_zero_seven(Pieces piece)
        {
            List<Move> diagonalMoves = new List<Move>();


            var posX = piece.CurrentPosition._PosX;
            var posY = piece.CurrentPosition._PosY;

            var x = posX;
            var y = posY;
            if (x == 7)
                x = 6;
            if (y == 0)
                y = 1;
            var direction = 1;

            do
            {
                x += direction;
                y -= direction;

                if (EncounterAlly(x, y,piece))
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

                else if (EncounterEnemy(x, y,piece))
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

            } while (x >= 0 || y <= 7 );
            for (int i = 0; i < diagonalMoves.Count; i++)
            {

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
                if (EncounterEnemy(x, positionY,piece))
                {
                    horizontalMoves.Add(new Move(x, positionY, 0));

                    if (x >= positionX && x <= 7)
                    {
                        x = positionX - 1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (EncounterAlly(x, positionY,piece))
                {
                    if (x >= positionX)
                    {
                        x = positionX - 1;
                        direction = -1;
                    }
                    else
                        break;
                }

                else if (x >= 7)
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

            for (int y = positionY; y >= 0; y += direction)
            {
              
                if (EncounterEnemy(positionX, y,piece))
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
                else if (EncounterAlly(positionX, y,piece))
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

                else if (y > 0 && y < 7)
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
                {
                    if (item.PieceColor != piece.PieceColor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool EncounterAlly(int x, int y, Pieces piece)
        {
            foreach (var item in SnapShotOfGameboard)
            {
                if (item.CurrentPosition._PosX == x && item.CurrentPosition._PosY == y)
                {
                    if (item.PieceColor == piece.PieceColor)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool WillItChessYou(Pieces piece, Move move, List<Pieces> gameboard)
        {
            var saveCurrentPos = new Point(0,0);
            saveCurrentPos._PosX = piece.CurrentPosition._PosX;
            saveCurrentPos._PosY = piece.CurrentPosition._PosY;
            var AmazingKing = new Queen(piece.PieceColor, FindMeKing(SnapShotOfGameboard,piece));
            piece.CurrentPosition._PosX = move.endPositions._PosX;
            piece.CurrentPosition._PosY = move.endPositions._PosY;

            QueenMovement(AmazingKing);

            for (int i = 0; i < AmazingKing.ListOfMoves.Count; i++)
            {
                for (int j = 0; j < gameboard.Count ; j++)
                {
                    if (gameboard[j].PieceColor != AmazingKing.PieceColor)
                    {
                        if (gameboard[j].CurrentPosition._PosX == move.endPositions._PosX && gameboard[j].CurrentPosition._PosY == move.endPositions._PosY)
                        {
                            if (gameboard[j].PieceType == ChessPieceSymbol.Bishop || gameboard[j].PieceType == ChessPieceSymbol.Queen || gameboard[j].PieceType == ChessPieceSymbol.Rook)
                            {
                                piece.CurrentPosition._PosX = saveCurrentPos._PosX;
                                piece.CurrentPosition._PosY = saveCurrentPos._PosY;
                                return true;
                            }
                        }
                    }
                }
            
            }
            //foreach (var item in AmazingKing.ListOfMoves)
            //{
            //    foreach (var opponent in SnapShotOfGameboard)
            //    {
            //        if (opponent.PieceColor != AmazingKing.PieceColor)
            //        {
            //            if (opponent.CurrentPosition == move.endPositions)
            //            {
            //                if (opponent.PieceType == ChessPieceSymbol.Bishop || opponent.PieceType == ChessPieceSymbol.Queen || opponent.PieceType == ChessPieceSymbol.Rook)
            //                {
            //                    piece.CurrentPosition = saveCurrentPos;
            //                    return true;
            //                }
            //            }
            //        }
            //    }
            //}
            piece.CurrentPosition._PosX = saveCurrentPos._PosX;
            piece.CurrentPosition._PosY = saveCurrentPos._PosY;
            return false;



        }
        private Point FindMeKing(List<Pieces> gameboard, Pieces Piece)
        {
            var point = new Point(0,0);
            foreach (var item in gameboard)
            {
                if (item.PieceType == ChessPieceSymbol.King)
                    if (Piece.PieceColor == item.PieceColor)
                    {
                        point._PosX = item.CurrentPosition._PosX;
                        point._PosY = item.CurrentPosition._PosY;
                        return point;
                    }
            }
            return point;
        }
        
        private void MoveOutOfChess(Pieces piece, List<Pieces> gameboard)
        {
            foreach (var item in piece.ListOfMoves)
            {
               if (!WillItChessYou(piece, item,gameboard));
                piece.ListOfMoves.Remove(item);
            }
        }
        public bool IsItChess(int turncounter)
        {
            if (turncounter % 2 == 0)
            {
                foreach (var item in SnapShotOfGameboard)
                {
                    foreach (var move in item.ListOfMoves)
                    {
                        if (item.PieceColor == Color.White)
                            if (item.PieceType == ChessPieceSymbol.King)
                                if (move.endPositions == item.CurrentPosition)
                                    return true;



                    }
                }
            }
            else if (turncounter % 2 == 1)
            {
                foreach (var item in SnapShotOfGameboard)
                {
                    foreach (var move in item.ListOfMoves)
                    {
                        if (item.PieceColor == Color.Black)
                            if (item.PieceType == ChessPieceSymbol.King)
                                if (move.endPositions == item.CurrentPosition)
                                    return true;



                    }
                }
            }
            return false;
        }
    }
}
