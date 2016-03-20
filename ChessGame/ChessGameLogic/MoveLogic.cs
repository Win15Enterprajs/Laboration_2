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
            //if (piece is Pawn)
            //    PawnMovement2(piece);

            //else if (piece is Rook)
            //    RookMovement(piece);

            //else if (piece is Queen)
            //    QueenMovement(piece);

            //else if (piece is King)
            //    KingMovement(piece);

            //else if (piece is Horse)
            //    HorseMovement(piece);

            //else if (piece is Bishop)
            //    BishopMovement(piece);

            piece.ListOfMoves = new List<Move>(Returnlistofmoves(piece, gameBoard));

            bool fuckyouchess = AmIInChess(piece, gameBoard);
            if (fuckyouchess)
            {
                GetMovesThatCancelChess(gameBoard, piece);
            }
            //piece.ListOfMoves = new List<Move>(templist);
            //piece.ListOfMoves = new List<Move>(Returnlistofmoves(piece, gameBoard));
        }
        private List<Move> Returnlistofmoves(Pieces piece, List<Pieces> gameBoard)
        {
            templist.Clear();

            if (piece is Pawn)
                PawnMovement2(piece);

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
        public void ClearMovementList(List<Pieces> gameboard)
        {
            foreach (var piece in gameboard)
            {
                piece.ListOfMoves.Clear();
            }
        }
        private void PawnMovement2(Pieces pawn)
        {
            List<Move> pawnMoveList = new List<Move>();
            var x = pawn.CurrentPosition._PosX;
            var y = pawn.CurrentPosition._PosY;
            if (pawn.PieceColor == Color.White)
            {
                pawnMoveList.Add((new Move((x), (y + 1), 0)));
                pawnMoveList.Add((new Move((x + 1), (y + 1), 0)));
                pawnMoveList.Add((new Move((x - 1), (y + 1), 0)));
                if (pawn.hasBeenMoved == false)
                {
                    pawnMoveList.Add((new Move((x), (y + 2), 0)));
                }
            }
            else if (pawn.PieceColor == Color.Black)
            {
                pawnMoveList.Add((new Move((x), (y - 1), 0)));
                pawnMoveList.Add((new Move((x + 1), (y - 1), 0)));
                pawnMoveList.Add((new Move((x - 1), (y - 1), 0)));
                if (pawn.hasBeenMoved == false)
                {
                    pawnMoveList.Add((new Move((x), (y - 2), 0)));
                }
            }
            for (int i = 0; i < pawnMoveList.Count; i++)
            {
                x = pawnMoveList[i].endPositions._PosX;
                y = pawnMoveList[i].endPositions._PosY;
                if (x <= 7 && x >= 0 && y >= 0 && y <= 7)
                {
                    if (EncounterAlly(x, y, pawn))
                    {

                    }
                    else if (EncounterEnemy(x, y, pawn))
                    {
                        if (x > pawn.CurrentPosition._PosX || x < pawn.CurrentPosition._PosX)
                        {
                            templist.Add(pawnMoveList[i]);
                        }
                    }
                    else
                    {
                        if (x == pawn.CurrentPosition._PosX)
                        {
                            if (x == (pawn.CurrentPosition._PosX + 2) || x == (pawn.CurrentPosition._PosX - 2))
                            {
                                if (pawn.PieceColor == Color.White)
                                {
                                    if (EncounterAlly(x + 1, y, pawn))
                                    {

                                    }
                                    else
                                    {
                                        templist.Add(pawnMoveList[i]);
                                    }
                                }
                                else if (pawn.PieceColor == Color.Black)
                                {
                                    if (EncounterAlly(x - 1,y,pawn))
                                    {

                                    }
                                    else
                                    {
                                        templist.Add(pawnMoveList[i]);
                                    }
                                }
                                
                            }
                            templist.Add(pawnMoveList[i]);
                        }
                    }
                }
            }
        }
        private void PawnMovement(Pieces pawn)
        {
            var positionX = pawn.CurrentPosition._PosX;
            var positionY = pawn.CurrentPosition._PosY;
            var noEncounterOnFirstMove = false;


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

            };
            for (int i = 0; i < pawnMoveList.Count; i++)
            {
                var posX = pawnMoveList[i].endPositions._PosX;
                var posY = pawnMoveList[i].endPositions._PosY;

                if ((posX >= 0 && positionX <= 7) && posY >= 0 && positionY <= 7)
                {
                    if ((!EncounterAlly(posX, posY, pawn)) && ((!EncounterEnemy(posX, posY, pawn))) && (posX == positionX))
                    {                      
                                templist.Add(pawnMoveList[i]);

                         
                    }
                    else if (EncounterEnemy(posX, posY, pawn) && posX != positionX)
                    {
                        templist.Add(pawnMoveList[i]);
                    }
                } 
            }
            
        }

        private void RookMovement(Pieces rook)
        {
            templist.AddRange(AddHorizontalAndVerticalMoves(rook));
        }

        private void QueenMovement(Pieces queen)
        {
            templist.AddRange(AddHorizontalAndVerticalMoves(queen));
            templist.AddRange(AddDiagonalMove(queen));
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
        }

        private void BishopMovement(Pieces bishop)
        {
            templist.AddRange(AddDiagonalMove(bishop));
        }
        private List<Move> AddHorizontalAndVerticalMoves(Pieces piece)
        {
            List<Move> horizontalAndVerticalMoves = new List<Move>();
            bool withinbounds = true;
            int x = piece.CurrentPosition._PosX;
            int y = piece.CurrentPosition._PosY;

            do
            {
                // This forloop looks for positive changes on the Y axis
                for (int i = 0; i < 7; i++)
                {
                    y++;
                    if (y <= 7)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece))
                        {
                            break;
                        }
                        // If the tile is not the original starting position adds the tile ot the movelist
                        else
                        {
                            if (x != piece.CurrentPosition._PosX)
                            {
                                horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            }
                        }
                    }
                    else
                        break;
                }
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                // This forloop looks for negative changes on the Y axis
                for (int i = 0; i < 7; i++)
                {
                    y--;
                    if (y >= 0)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece))
                        {
                            break;
                        }
                        // If the tile is not the original starting position adds the tile ot the movelist
                        else
                        {
                            if (y != piece.CurrentPosition._PosY)
                            {
                                horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            }
                        }
                    }
                    else
                        break;
                }
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                // This forloop looks for positive changes on the X axis
                for (int i = 0; i < 7; i++)
                {
                    x++;
                    if (x <= 7)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece))
                        {
                            break;
                        }
                        // If the tile is not the original starting position adds the tile ot the movelist
                        else
                        {
                            if (x != piece.CurrentPosition._PosX)
                            {
                                horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            }
                        }
                    }
                    else
                        break;
                }
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                // This forloop looks for positive changes on the Y axis
                for (int i = 0; i < 7; i++)
                {
                    x--;
                    if (x >= 0)
                    {
                        if (EncounterEnemy(x, y, piece))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece))
                        {
                            break;
                        }
                        // If the tile is not the original starting position adds the tile ot the movelist
                        else
                        {
                            if (x != piece.CurrentPosition._PosX)
                            {
                                horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            }
                        }
                    }
                    else
                        break;
                }
                withinbounds = false;
            } while (withinbounds);
        

            return horizontalAndVerticalMoves;
        }
    
        private List<Move> AddDiagonalMove(Pieces piece)
        {
            List<Move> diagonalmoves = new List<Move>();
            bool withinbounds = true;
            int x = piece.CurrentPosition._PosX;
            int y = piece.CurrentPosition._PosY;
            do
            {

                // Looping through spaces up to the right until you run out of gameboard or encounter an enemy or ally
                for (int i = 0; i < 6; i++)
                {
                    // Makes sure the initial values are within bounds of the gameboard
                    if (x <= 7 && y <= 7)
                    {
                        // Manipulate the values in the direction we want and check if they are within board
                        x++;
                        y++;
                        if (x <= 7 && y <= 7)
                        {
                            // Checks if the newfound tile has an enemy in it
                            if (EncounterEnemy(x, y, piece))
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                                break;
                            }
                            // checks if the newfound tile has an ally in it
                            else if (EncounterAlly(x, y, piece))
                            {
                                break;
                            }
                            // If the tile is not the original starting position adds the tile ot the movelist
                            else
                            {
                                if (x != piece.CurrentPosition._PosX && y != piece.CurrentPosition._PosY)
                                {
                                    diagonalmoves.Add(new Move(x, y, 0));
                                }
                            }
                        }
                        else
                            break;
                    }
                   
                }
                // adding moves down left
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                for (int i = 0; i < 6; i++)
                {
                    if (x >= 0 && y >= 0)
                    {
                        x--;
                        y--;
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
                            }
                        }
                        else
                            break;
                    }
                }
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                // adding moves down to the right
                for (int i = 0; i < 6; i++)
                {
                    if (x <= 7 && y >= 0)
                    {
                        x++;
                        y--;
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
                            }
                        }
                        else
                            break;
                    }
                    
                }
                x = piece.CurrentPosition._PosX;
                y = piece.CurrentPosition._PosY;
                // adding moves up to the left
                for (int i = 0; i < 6; i++)
                {
                    if (x >= 0 && y <= 7)
                    {
                        x--;
                        y++;
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
                            }
                        }
                        else
                            break;
                    }
                }
                withinbounds = false;
            } while (withinbounds);
            return diagonalmoves;
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
                for (int j = 0; j < gameboard.Count; j++)
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
       
        private bool AmIInChess(Pieces piece, List<Pieces> gameboard)
        {

            var move = new Move(FindMeKing(gameboard, piece), 0);
            //var move = new Move(FindMeKing(gameboard, piece)._PosX, FindMeKing(gameboard, piece)._PosY, 0);
            List<Move> tempmovelist = new List<Move>();
            for (int i = 0; i < gameboard.Count; i++)
            {
                if (piece.PieceColor != gameboard[i].PieceColor)
                {
                    gameboard[i].ListOfMoves.AddRange(Returnlistofmoves(gameboard[i], gameboard));
                }
            }
          
            for (int i = 0; i < gameboard.Count; i++)
            {
                for (int j = 0; j < gameboard[i].ListOfMoves.Count; j++)
                {
                    tempmovelist.Add(gameboard[i].ListOfMoves[j]);
                }
            }
            for (int i = 0; i < gameboard.Count; i++)
            {
                if (gameboard[i].PieceColor != piece.PieceColor)
                {
                    gameboard[i].ListOfMoves.Clear();
                }
            }
            for (int i = 0; i < tempmovelist.Count; i++)
            {
                if (tempmovelist[i].endPositions._PosX == move.endPositions._PosX && tempmovelist[i].endPositions._PosY == move.endPositions._PosY)
                    return true;
            }
                return false;
        }
        private void GetMovesThatCancelChess(List<Pieces> gameboard, Pieces piece)
        {
            var newlistOfValidMoves = new List<Move>();
            for (int i = 0; i < piece.ListOfMoves.Count; i++)
            {
                if (WillThisMoveCancelChess(gameboard, piece, piece.ListOfMoves[i]))
                {
                    newlistOfValidMoves.Add(piece.ListOfMoves[i]);

                }

            }

            piece.ListOfMoves.Clear();
            piece.ListOfMoves = new List<Move>(newlistOfValidMoves);
        }
        private bool WillThisMoveCancelChess(List<Pieces> gameboard, Pieces piece, Move move)
        {
            var savex = piece.CurrentPosition._PosX;
            var savey = piece.CurrentPosition._PosY;

            piece.CurrentPosition._PosX = move.endPositions._PosX;
            piece.CurrentPosition._PosY = move.endPositions._PosY;

            if (AmIInChess(piece, gameboard))
            {
                piece.CurrentPosition._PosX = savex;
                piece.CurrentPosition._PosY = savey;
                return false;
            }

            else
            {
                piece.CurrentPosition._PosX = savex;
                piece.CurrentPosition._PosY = savey;
                return true;
            }

        }
        
    }
}
