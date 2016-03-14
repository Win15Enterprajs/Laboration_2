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
        public string Color { get; set; }
        public Point CurrentPosition { get; set; }
        public List<Move> ListOfMoves;
        public bool hasBeenMoved = false;
        public ChessPieceSymbol PieceType;


        public Pieces(string color, Point currentPosition, ChessPieceSymbol type)
        {
            this.PieceType = type;
            this.Color = color;
            this.CurrentPosition = currentPosition;
        }

        public Pieces(Point currentPosition, string color, bool hasBennMoved, ChessPieceSymbol type)
        {
            this.PieceType = type;
            this.Color = color;
            this.CurrentPosition = currentPosition;
            this.hasBeenMoved = false;
        }

        public List<Move> ReturnMoveList()
        {
            return ListOfMoves;
        }
    }
}