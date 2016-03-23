using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic.ChessPieces
{

    partial class Ai2
    {
        MoveLogic AiMove = new MoveLogic();
        private List<Pieces> Enemies;
        private List<Pieces> Allies;
        //////////////////////////////////////////////////////
        private List<Pieces> GameBoard;
        private List<Pieces> TempGameBoard;


        public Ai2(List<Pieces> gameBoard)
        {
            this.GameBoard = gameBoard;
        }

        // initierar AI´n (sepererar allies i enemies m.m)
        public void InitiateAI(Pieces piece)
        {   //creates the gameboard that the Ai will use as reference. 
            ///////////////////////////////////////////// 
           
            TempGameBoard = new List<Pieces>(GameBoard);

            /////////////////////////////////////////////
            // seperates the allies and enemies into different lists. (for simplicity)
            Allies = GetAllies(piece.PieceColor);
            Enemies = GetEnemies(piece.PieceColor);
            SetMovesForEnemies();
        }

        //bas Metoden.. typish.. 
        public void GiveValueToMoves(Pieces piece)
        {
            foreach (Move move in piece.ListOfMoves)
            {
                if (CanItakeSomething(move))
                {
                    GiveTakeValue(move);
                    WillIgetThreatened(move);
                }
            }
        }

        //kollar bara om ett moves kan ta något
        private bool CanItakeSomething(Move move)
        {
            foreach (Pieces E in Enemies)
            {

                if (CompareEnemyPositionToMyMove(E.CurrentPosition, move))
                {
                    return true;
                }
            }
            return false;
        }

        // ger movet värde beroende på vad den tar
        private void GiveTakeValue(Move move)
        {
            foreach (Pieces enemy in Enemies)
            {
                if (CompareEnemyPositionToMyMove(enemy.CurrentPosition, move))
                {
                    move.value += enemy.Value;
                }
            }
        }

        // precis som det låter, kollar om movet kommer bli hotat av en motståndare
        private bool WillIgetThreatened(Move move)
        {

            return false;
        }

        // kollar om ett move är skyddat av en annan allie.
        private bool WillIBeProtected(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        // kollar om movet kommer hota en annan pjäs
        private bool WillIthreaten(Move move)
        {
            return false;
        }

        // är jag skyddad här?!
        private bool AmIProtected(Pieces piece, List<Pieces> board)
        {
            return false;
        }

        // är jag hotad här?!
        private bool AmIThreatened(Pieces piece)
        {
            foreach (var enemy in Enemies)
            {
                foreach (var move in enemy.ListOfMoves)
                {
                    if(CompareEnemyMoveWithCurrentPosition(move, piece.CurrentPosition))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // kan jag hota kungen?!
        private bool CanIThreatenDaKing()
        {
            return false;
        }


        // om man gör ändringar för att kolla olika vilkor, tex (willthis move threaten something) så kan man resetta tempgameboard till "gameBoard"
        private void RestoreTempGameBoard()
        {
            TempGameBoard = new List<Pieces>(GameBoard);
        }

        // eftersom ennemy inte har några moves, så får dom de här! 
        private void SetMovesForEnemies()
        {
            foreach (Pieces P in Enemies)
            {
                AiMove.SetMovementList(P, GameBoard);
            }
        }


        // gämnför två Moves och returnerar true eller false beroende på om de innehåller samma koordinater eller inte. 
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
        // separerar ennemies från allies, lägger dom i egna listor(för enkelhets skull. gör också så att man inte behöver ha fula checkar över allt)
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

        public void MakeCopyOfGameboard()
        {
            TempGameBoard.Clear();

            var pawns = GameBoard.Where(x => x is Pawn).ToList();
            pawns.ForEach(x => TempGameBoard.Add(new Pawn(x.PieceColor, x.CurrentPosition, x.hasBeenMoved)));

            var bishops = GameBoard.Where(x => x is Bishop).ToList();
            bishops.ForEach(x => TempGameBoard.Add(new Bishop(x.PieceColor, x.CurrentPosition)));

            var horses = GameBoard.Where(x => x is Horse).ToList();
            horses.ForEach(x => TempGameBoard.Add(new Horse(x.PieceColor, x.CurrentPosition)));

            var rooks = GameBoard.Where(x => x is Rook).ToList();
            rooks.ForEach(x => TempGameBoard.Add(new Rook(x.PieceColor, x.CurrentPosition, x.hasBeenMoved)));

            var queens = GameBoard.Where(x => x is Queen).ToList();
            queens.ForEach(x => TempGameBoard.Add(new Queen(x.PieceColor, x.CurrentPosition)));

            var kings = GameBoard.Where(x => x is King).ToList();
            kings.ForEach(x => TempGameBoard.Add(new King(x.PieceColor, x.CurrentPosition)));


        }




    }
}










