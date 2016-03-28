using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
   partial class Ai2
    {
        public void InitiateAI(Color color)
        {   //creates the gameboard that the Ai will use as reference. 
            ///////////////////////////////////////////// 
            MakeCopyOfGameboard();
            /////////////////////////////////////////////
            // seperates the allies and enemies into different lists. (for simplicity)
            Allies = GetAllies(color);
            Enemies = GetEnemies(color);
            SetMovesForEnemies(color);
        }

        // om man gör ändringar för att kolla olika vilkor, tex (willthis move threaten something) så kan man resetta tempgameboard till "gameBoard"
        // Resets the tempgameboard to the standard gameboard.
        private void RestoreTempGameBoard()
        {
            MakeCopyOfGameboard();
        }


       
        // Set moves to the enemy.
        private void SetMovesForEnemies(Color color)
        {
            for (int i = 0; i < TempGameBoard.Count; i++)
            {
                if (TempGameBoard[i].PieceColor != color)
                    AiMove.SetMovementList(TempGameBoard[i], TempGameBoard);
            }
        }


        private Pieces GetPieceFromTempBoard(Pieces piece)
        {
            Pieces tempPiece = null;
            for (int i = 0; i < TempGameBoard.Count; i++)
            {
                if (TempGameBoard[i].CurrentPosition._PosX == piece.CurrentPosition._PosX &&
                    TempGameBoard[i].CurrentPosition._PosY == piece.CurrentPosition._PosY)
                    tempPiece = TempGameBoard[i];
            }

            return tempPiece;
        }
        
        // Compares two moves and return true or false depending on if it contains the same coordinates.
        #region CompareMoves()...
        public bool CompareEnemyMoveToMyMove(Move enemyMove, Move move)
        {
            if (enemyMove.endPositions._PosX == move.endPositions._PosX && enemyMove.endPositions._PosY == move.endPositions._PosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CompareEnemyPositionToMyMove(Point enemyPosition, Move move)
        {
            if (enemyPosition._PosX == move.endPositions._PosX && enemyPosition._PosY == move.endPositions._PosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CompareEnemyMoveWithCurrentPosition(Move enemyMove, Point myPosition)
        {
            if (enemyMove.endPositions._PosX == myPosition._PosX && enemyMove.endPositions._PosY == myPosition._PosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        
        // Seperate enemies from allies and puts them in their own lists.
        #region Seperate Enemies and Allies
        private List<Pieces> GetEnemies(Color color)
        {
            return GameBoard.Where(P => P.PieceColor != color).ToList();
        }
        private List<Pieces> GetAllies(Color color)
        {
            return GameBoard.Where(P => P.PieceColor == color).ToList();
        }
        #endregion

        // Check the current position of enemy king and returns the enemy kings position
        private Point GetPositionOfEnemyKing()
        {
            var enemyKing = Enemies.Find(x => x is King);
            Point enemyKingPosition = new Point(enemyKing.CurrentPosition._PosX, enemyKing.CurrentPosition._PosY);
            return enemyKingPosition;
        }

        // Makes a complete copy of the current gameboard
        private void MakeCopyOfGameboard()
        {
            TempGameBoard.Clear();

            var pawns = GameBoard.Where(x => x is Pawn).ToList();
            pawns.ForEach(x => TempGameBoard.Add( new Pawn(x.PieceColor, new Point(x.CurrentPosition._PosX, x.CurrentPosition._PosY), x.hasBeenMoved)));

            var bishops = GameBoard.Where(x => x is Bishop).ToList();
            bishops.ForEach(x => TempGameBoard.Add(new Bishop(x.PieceColor, new Point(x.CurrentPosition._PosX, x.CurrentPosition._PosY))));

            var horses = GameBoard.Where(x => x is Horse).ToList();
            horses.ForEach(x => TempGameBoard.Add(new Horse(x.PieceColor, new Point(x.CurrentPosition._PosX, x.CurrentPosition._PosY))));

            var rooks = GameBoard.Where(x => x is Rook).ToList();
            rooks.ForEach(x => TempGameBoard.Add(new Rook(x.PieceColor, new Point(x.CurrentPosition._PosX, x.CurrentPosition._PosY), x.hasBeenMoved)));

            var queens = GameBoard.Where(x => x is Queen).ToList();
            queens.ForEach(x => TempGameBoard.Add(new Queen(x.PieceColor, new Point(x.CurrentPosition._PosX, x.CurrentPosition._PosY))));

            var kings = GameBoard.Where(x => x is King).ToList();
            kings.ForEach(x => TempGameBoard.Add(new King(x.PieceColor, new Point(x.CurrentPosition._PosX, x.CurrentPosition._PosY))));


        }
        private void GiveRandomValueToAMove(Move move)
        {
            int nr = rnd.Next(0, 10);
            move.value += nr;
        }
        private void PawnMoveToPromotion(Pieces piece)
        {
            if (piece is Pawn && TempGameBoard.Count < 8)
            {
                piece.ListOfMoves.ForEach(x => x.value += 30);
            }
        }

    }
}
