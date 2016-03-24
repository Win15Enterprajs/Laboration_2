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
        //List<Pieces> SnapShotOfGameboard;
        private List<Move> templist = new List<Move>();

        public void SetMovementList(Pieces piece, List<Pieces> gameBoard)
        {
            //SnapShotOfGameboard = new List<Pieces>(gameBoard);
            templist.Clear();
            piece.ListOfMoves = new List<Move>(Returnlistofmoves(piece, gameBoard));

            bool Chess = AmIInChess(piece.PieceColor, gameBoard);
            if (Chess)
            {
                GetMovesThatCancelChess(gameBoard, piece);
            }
            else
            {
                RemoveMovesThatWillChessYou(piece, gameBoard);
            }
        }
        private void RemoveMovesThatWillChessYou(Pieces piece, List<Pieces> gameboard)
        {
            var savex = piece.CurrentPosition._PosX;
            var savey = piece.CurrentPosition._PosY;
            var newlistOfValidMoves = new List<Move>();

            for (int i = 0; i < piece.ListOfMoves.Count; i++)
            {
                if (WillItChessYou(piece,piece.ListOfMoves[i], gameboard))
                {
                    newlistOfValidMoves.Add(piece.ListOfMoves[i]);
                }
            }
            piece.ListOfMoves.Clear();
            piece.ListOfMoves = new List<Move>(newlistOfValidMoves);
        }
        private List<Move> Returnlistofmoves(Pieces piece, List<Pieces> gameBoard)
        {
            templist.Clear();

            if (piece is Pawn)
                PawnMovement(piece, gameBoard);

            else if (piece is Rook)
                RookMovement(piece, gameBoard);

            else if (piece is Queen)
                QueenMovement(piece, gameBoard);

            else if (piece is King)
                KingMovement(piece, gameBoard);

            else if (piece is Horse)
                HorseMovement(piece, gameBoard);

            else if (piece is Bishop)
                BishopMovement(piece, gameBoard);

           
            return templist;
        }
        public void ClearMovementList(List<Pieces> gameboard)
        {
            foreach (var piece in gameboard)
            {
                piece.ListOfMoves.Clear();
            }
        }
        private void PawnMovement(Pieces pawn, List<Pieces> gameboard)
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
                    if (EncounterAlly(x, y, pawn, gameboard))
                    {

                    }
                    else if (EncounterEnemy(x, y, pawn, gameboard))
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
                                    if (EncounterAlly(x + 1, y, pawn, gameboard))
                                    {

                                    }
                                    else
                                    {
                                        templist.Add(pawnMoveList[i]);
                                    }
                                }
                                else if (pawn.PieceColor == Color.Black)
                                {
                                    if (EncounterAlly(x - 1,y,pawn, gameboard))
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

        private void RookMovement(Pieces rook, List<Pieces> gameboard)
        {
            templist.AddRange(AddHorizontalAndVerticalMoves(rook, gameboard));
        }

        private void QueenMovement(Pieces queen, List<Pieces> gameboard)
        {
            templist.AddRange(AddHorizontalAndVerticalMoves(queen, gameboard));
            templist.AddRange(AddDiagonalMove(queen, gameboard));
        }

        private void KingMovement(Pieces king, List<Pieces> gameboard)
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
                if (!(EncounterAlly(kingMoveList[i].endPositions._PosX, kingMoveList[i].endPositions._PosY, king, gameboard)))
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

        private void HorseMovement(Pieces horse, List<Pieces> gameboard)
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
                if (!(EncounterAlly(horseMoveList[i].endPositions._PosX, horseMoveList[i].endPositions._PosY, horse, gameboard)))
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

        private void BishopMovement(Pieces bishop, List<Pieces> gameboard)
        {
            templist.AddRange(AddDiagonalMove(bishop, gameboard));
        }
        private List<Move> AddHorizontalAndVerticalMoves(Pieces piece, List<Pieces> gameboard)
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
                        if (EncounterEnemy(x, y, piece, gameboard))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece, gameboard))
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
                // This forloop looks for negative changes on the Y axis
                for (int i = 0; i < 7; i++)
                {
                    y--;
                    if (y >= 0)
                    {
                        if (EncounterEnemy(x, y, piece, gameboard))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece, gameboard))
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
                        if (EncounterEnemy(x, y, piece, gameboard))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece, gameboard))
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
                // This forloop looks for positive changes on the X axis
                for (int i = 0; i < 7; i++)
                {
                    x--;
                    if (x >= 0)
                    {
                        if (EncounterEnemy(x, y, piece, gameboard))
                        {
                            horizontalAndVerticalMoves.Add(new Move(x, y, 0));
                            break;
                        }
                        // checks if the newfound tile has an ally in it
                        else if (EncounterAlly(x, y, piece, gameboard))
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
    
        private List<Move> AddDiagonalMove(Pieces piece, List<Pieces> gameboard)
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
                            if (EncounterEnemy(x, y, piece, gameboard))
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                                break;
                            }
                            // checks if the newfound tile has an ally in it
                            else if (EncounterAlly(x, y, piece, gameboard))
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
                            if (EncounterEnemy(x, y, piece, gameboard))
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                                break;
                            }
                            else if (EncounterAlly(x, y, piece, gameboard))
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
                            if (EncounterEnemy(x, y, piece, gameboard))
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                                break;
                            }
                            else if (EncounterAlly(x, y, piece, gameboard))
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
                            if (EncounterEnemy(x, y, piece, gameboard))
                            {
                                diagonalmoves.Add(new Move(x, y, 0));
                                break;
                            }
                            else if (EncounterAlly(x, y, piece, gameboard))
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
        
        private bool EncounterEnemy(int x, int y,Pieces piece, List<Pieces> gameboard)
        {
            foreach (var item in gameboard)
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
        private bool EncounterAlly(int x, int y, Pieces piece, List<Pieces> gameboard)
        {
            foreach (var item in gameboard)
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
           bool letssee = WillThisMoveCancelChess(gameboard, piece, move);
            //var saveCurrentPos = new Point(0,0);
            //saveCurrentPos._PosX = piece.CurrentPosition._PosX;
            //saveCurrentPos._PosY = piece.CurrentPosition._PosY;
            ////var AmazingKing = new Queen(piece.PieceColor, FindMeKing(gameboard,piece));
            //piece.CurrentPosition._PosX = move.endPositions._PosX;
            //piece.CurrentPosition._PosY = move.endPositions._PosY;
            //var AmazingKing = new Queen(piece.PieceColor, FindMeKing(gameboard, piece));

            //QueenMovement(AmazingKing);
            //AmazingKing.ListOfMoves = new List<Move>(templist);
            //templist.Clear();

            //for (int i = 0; i < AmazingKing.ListOfMoves.Count; i++)
            //{
            //    for (int j = 0; j < gameboard.Count; j++)
            //    {
            //        if (gameboard[j].PieceColor != AmazingKing.PieceColor)
            //        {
            //            if (gameboard[j].CurrentPosition._PosX == move.endPositions._PosX && gameboard[j].CurrentPosition._PosY == move.endPositions._PosY)
            //            {
            //                if (gameboard[j].PieceType == ChessPieceSymbol.Bishop || gameboard[j].PieceType == ChessPieceSymbol.Queen || gameboard[j].PieceType == ChessPieceSymbol.Rook)
            //                {
            //                    piece.CurrentPosition._PosX = saveCurrentPos._PosX;
            //                    piece.CurrentPosition._PosY = saveCurrentPos._PosY;
            //                    return true;
            //                }
            //            }
            //        }
            //    }

            //}
            //piece.CurrentPosition._PosX = saveCurrentPos._PosX;
            //piece.CurrentPosition._PosY = saveCurrentPos._PosY;
            //return false;
            return letssee;



        }
        private Point FindMeKing(List<Pieces> gameboard, Color Color)
        {
            var point = new Point(0,0);
            foreach (var item in gameboard)
            {
                if (item.PieceType == ChessPieceSymbol.King)
                    if (Color == item.PieceColor)
                    {
                        point._PosX = item.CurrentPosition._PosX;
                        point._PosY = item.CurrentPosition._PosY;
                        return point;
                    }
            }
            return point;
        }
        
        //private void MoveOutOfChess(Pieces piece, List<Pieces> gameboard)
        //{
        //    foreach (var item in piece.ListOfMoves)
        //    {
        //       if (!WillItChessYou(piece, item,gameboard));
        //        piece.ListOfMoves.Remove(item);
        //    }
        //}
       
        private bool AmIInChess(Color color, List<Pieces> gameboard)
        {

            var move = new Move(FindMeKing(gameboard, color), 0);
            List<Move> tempmovelist = new List<Move>();
            for (int i = 0; i < gameboard.Count; i++)
            {
                if (color != gameboard[i].PieceColor)
                {
                    gameboard[i].ListOfMoves.AddRange(Returnlistofmoves(gameboard[i], gameboard));
                }
            }
          
            for (int i = 0; i < gameboard.Count; i++)
            {
                for (int j = 0; j < gameboard[i].ListOfMoves.Count; j++)
                {
                    if (color != gameboard[i].PieceColor)
                    {
                        tempmovelist.Add(gameboard[i].ListOfMoves[j]);
                    }
                }
            }
            for (int i = 0; i < gameboard.Count; i++)
            {
                if (gameboard[i].PieceColor != color)
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
            var gameboardtest = new List<Pieces>(gameboard);
            Pieces enemypiecesaved = null;

            for (int i = 0; i < gameboardtest.Count; i++)
            {
                if (move.endPositions._PosX == gameboardtest[i].CurrentPosition._PosX && move.endPositions._PosY == gameboardtest[i].CurrentPosition._PosY)
                {
                    enemypiecesaved = gameboardtest[i];
                    gameboardtest.Remove(gameboardtest[i]);
                    break;

                }
            }

            for (int i = 0; i < gameboardtest.Count; i++)
            {
                if (gameboardtest[i].CurrentPosition._PosX == savex && gameboardtest[i].CurrentPosition._PosY == savey)
                {
                    gameboardtest[i].CurrentPosition._PosX = move.endPositions._PosX;
                    gameboardtest[i].CurrentPosition._PosY = move.endPositions._PosY;
                    
                }
            }

            piece.CurrentPosition._PosX = move.endPositions._PosX;
            piece.CurrentPosition._PosY = move.endPositions._PosY;

            if (AmIInChess(piece.PieceColor, gameboardtest))
            {
                piece.CurrentPosition._PosX = savex;
                piece.CurrentPosition._PosY = savey;
                gameboardtest.Add(enemypiecesaved);
                return false;
            }

            else
            {
                piece.CurrentPosition._PosX = savex;
                piece.CurrentPosition._PosY = savey;
                gameboardtest.Add(enemypiecesaved);
                return true;
            }

    }
        public bool IsEnemyInCheck(int turn, List<Pieces> gameboard)
        {
            Color colorOfNextTurn;           
            var nextTurn = turn + 1;
            if (nextTurn % 2 == 1)
            {
                colorOfNextTurn = Color.White;
            }
            else
            {
                colorOfNextTurn = Color.Black;

            }
            return AmIInChess(colorOfNextTurn, gameboard);

        }

        public bool CheckGamestateForCheck(int turn, List<Pieces> gameBoard)
        {
            Color ColorThisTurn;
            if (turn % 2 == 1)
            {
               ColorThisTurn = Color.White;
            }
            else
            {
                ColorThisTurn = Color.Black;
            }
            return AmIInChess(ColorThisTurn, gameBoard );
        }

    }
}
