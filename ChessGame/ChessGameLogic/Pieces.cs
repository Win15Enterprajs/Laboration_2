using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;

namespace ChessGameLogic
{
    public abstract class Pieces
    {
        public double Value { get; set; }
        public Color PieceColor { get; set; }
        public Point CurrentPosition { get; set; }
        public Move BestMove { get; set; }
        public List<Move> ListOfMoves;
        public bool hasBeenMoved = false;
        public ChessPieceSymbol PieceType;


        public Pieces(Color color, Point currentPosition, ChessPieceSymbol type)
        {
            this.PieceType = type;
            this.PieceColor = color;
            this.CurrentPosition = currentPosition;
        }

        public Pieces(Point currentPosition, Color color, bool hasBennMoved, ChessPieceSymbol type)
        {
            this.PieceType = type;
            this.PieceColor = color;
            this.CurrentPosition = currentPosition;
            this.hasBeenMoved = false;
        }

        public List<Move> ReturnMoveList()
        {
            return ListOfMoves;
        }
    }
}