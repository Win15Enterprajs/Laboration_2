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
       

        private bool DoIThreaten(Move move)
        {
            foreach (Pieces enemy in Enemies)
            {
                if (CompareEnemyPositionToMyMove(enemy.CurrentPosition,move))
                {
                    return true;
                }
            }
            return false;
        }
            
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
            Pieces tempPiece = GetPieceFromTempBoard(piece);



            tempPiece.CurrentPosition = new Point(move.endPositions._PosX, move.endPositions._PosY);
            AiMove.SetMovementList(tempPiece, TempGameBoard);

            foreach (Move tempMove in tempPiece.ListOfMoves)
            {
                if (CanItakeSomething(tempMove)) 
                {
                    RestoreTempGameBoard();
                    return true;
                }
            }

            RestoreTempGameBoard();
            return false;
        }

        private bool WillIgetThreatened(Move move, Pieces piece)
        {

            var tempPiece = GetPieceFromTempBoard(piece);

            tempPiece.CurrentPosition = new Point(move.endPositions._PosX, move.endPositions._PosY);

            if (AmIThreatened(tempPiece))
            {
                RestoreTempGameBoard();
                return true;
            }
            else
            {
                RestoreTempGameBoard();
                return false;
            }
        }


        private List<Pieces> GetEnemiesThatIThreaten(Pieces piece)
        {
            var ListOfThreatnedEnemies = piece.ListOfMoves.Select(move => GetThretnedEnemyPiece(move)).ToList();
            return ListOfThreatnedEnemies;
        }


        private Pieces GetThretnedEnemyPiece(Move move)
        {
            Pieces tempPiece = Enemies.Find(p => (p.CurrentPosition._PosX == move.endPositions._PosX && p.CurrentPosition._PosY == move.endPositions._PosY));

            return tempPiece;


        }

        // kan jag hota kungen?!
        private bool CanIThreatenTheKing(Move possibleMove, Pieces Piece)
        {
            var testPiece = GetPieceFromTempBoard(Piece);

            testPiece.CurrentPosition._PosX = possibleMove.endPositions._PosX;
            testPiece.CurrentPosition._PosY = possibleMove.endPositions._PosY;

            AiMove.SetMovementList(testPiece, TempGameBoard);

            foreach (var move in testPiece.ListOfMoves)
            {
                if(CompareEnemyPositionToMyMove(GetPositionOfEnemyKing(), move))
                {
                    MakeCopyOfGameboard();
                    return false;
                }
            }
            MakeCopyOfGameboard();
            return false;


        }



    }
}
    