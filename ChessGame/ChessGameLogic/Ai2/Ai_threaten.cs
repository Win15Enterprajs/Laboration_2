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
        // är jag hotad här?!

        private bool AmIThreatened(Pieces piece)
        {
            foreach (var enemy in Enemies)
            {
                foreach (var move in enemy.ListOfMoves)
                {
                    if (CompareEnemyMoveWithCurrentPosition(move, piece.CurrentPosition))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool WillIthreaten(Pieces piece, Move move)
        {
            var tempPiece = GetPieceFromTempBoard(piece);

            tempPiece.CurrentPosition = new Point(move.endPositions._PosX, move.endPositions._PosY);
            AiMove.SetMovementList(tempPiece, TempGameBoard);

            foreach (Move tempMove in tempPiece.ListOfMoves)
            {
                if (CanItakeSomething(tempMove)) 
                {
                    move.value += GetThretnedEnemyPiece(tempMove).Value / 2;
                    return true;
                }
            }

            RestoreTempGameBoard();
            return false;
        }

        private bool WillIgetThreatened(Move move, Pieces piece)
        {

            var tempPiece = GetPieceFromTempBoard(piece);
            tempPiece.CurrentPosition = move.endPositions;

            if (AmIThreatened(tempPiece))
                return true;
            else
                return false;
        }


        private List<Pieces> GetEnemiesThatIThreaten(Pieces piece)
        {
            var ListOfThreatnedEnemies = piece.ListOfMoves.Select(move => GetThretnedEnemyPiece(move)).ToList();

            foreach (Pieces X in ListOfThreatnedEnemies.Where(X => X == null))
            {
                ListOfThreatnedEnemies.Remove(X);
            }
            return ListOfThreatnedEnemies;
        }


        private Pieces GetThretnedEnemyPiece(Move move)
        {
            Pieces tempPiece = TempGameBoard.Find(p => p.CurrentPosition == move.endPositions);
            
            return tempPiece;


        }

        // kan jag hota kungen?!
        private bool CanIThreatenDaKing()
        {
            return false;
        }



    }
}
