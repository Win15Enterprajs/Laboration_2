using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameLogic
{
    public class GameLogger : ILogger
    {
        public List<string> gameLog { get; }
        string LogPost = string.Empty;
        Dictionary<int, char> LetterConversion = new Dictionary<int, char>
        {
            [0] = 'A',
            [1] = 'B',
            [2] = 'C',
            [3] = 'D',
            [4] = 'E',
            [5] = 'F',
            [6] = 'G',
            [7] = 'H',
        };

        public GameLogger()
        {
            gameLog = new List<string>();
        }

        public void LogPieceToMove(Pieces pieceToMove)
        {
            var log = string.Format($"{pieceToMove.PieceColor} {pieceToMove.PieceType} from {LetterConversion[pieceToMove.CurrentPosition._PosX]}{pieceToMove.CurrentPosition._PosY + 1} to {LetterConversion[pieceToMove.BestMove.endPositions._PosX]}{pieceToMove.BestMove.endPositions._PosY + 1}");
            LogPost += log;
        }

        public void LogKilledPieceToRemove(Pieces killedPiece)
        {
            var log = string.Format($" | It took a {killedPiece.PieceColor} {killedPiece.PieceType}");
            LogPost += log;
        }

        public void LogTurn()
        {
            gameLog.Add(LogPost);
            LogPost = string.Empty;
        }
    }
}
