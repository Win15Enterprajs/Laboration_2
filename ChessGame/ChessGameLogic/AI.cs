﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGameLogic.Packages;
using System.Threading;

namespace ChessGameLogic
{
    class AI
    {
        MoveLogic tempmovelogic = new MoveLogic();
        // templista för att kunna skriva AI
        Random rnd = new Random();

        public /*List<Move>*/ void GiveValuetoMoves(Pieces piece,List<Pieces> tempgameboard)
        {
            for (int i = 0; i < piece.ListOfMoves.Count; i++)
            {
                if (CanThisMoveTakeSomething(piece.ListOfMoves[i], tempgameboard))
                {
                    GiveInitialTakeValue(piece.ListOfMoves[i],tempgameboard,piece);
                    RemoveSelfFromValue(piece.ListOfMoves[i], piece);
                }
                else
                {
                    GiveRandomValueToAMove(piece.ListOfMoves[i]);
                    PawnMoveToPromotion(piece, tempgameboard);
                    CanIThreatenTheKing(piece.ListOfMoves[i], piece, tempgameboard);
                }
            }             
        }
        private void GiveInitialTakeValue(Move move, List<Pieces> tempgameboard,Pieces piece)
        {
            foreach (var opponent in tempgameboard)
            {

                if (piece.PieceColor != opponent.PieceColor)
                {
                    if (opponent.PieceType == ChessPieceSymbol.Bishop)
                        move.value = 30;
                    if (opponent.PieceType == ChessPieceSymbol.Horse)
                        move.value = 30;
                    if (opponent.PieceType == ChessPieceSymbol.Pawn)
                        move.value = 30;
                    if (opponent.PieceType == ChessPieceSymbol.Queen)
                        move.value = 90;
                    if (opponent.PieceType == ChessPieceSymbol.Rook)
                        move.value = 50;
                }

            }
                
        }

        private void PawnMoveToPromotion(Pieces piece, List<Pieces> gb)
        {
            if (piece is Pawn && gb.Count < 8)
            {
                piece.ListOfMoves.ForEach(x => x.value += 10);
            }
        }

        private Move RemoveSelfFromValue(Move move, Pieces piece)
        {
            if (piece.PieceType == ChessPieceSymbol.Bishop)
                move.value = move.value - (30 * 0.25);
            if (piece.PieceType == ChessPieceSymbol.Horse)
                move.value = move.value - (30 * 0.25);
            if (piece.PieceType == ChessPieceSymbol.Queen)
                move.value = move.value - (90 * 0.25);
            if (piece.PieceType == ChessPieceSymbol.Rook)
                move.value = move.value - (30 * 0.25);
            if (piece.PieceType == ChessPieceSymbol.Pawn)
                    move.value = move.value - (10 * 0.25);
            return move;
            
        }
        private bool CanThisMoveTakeSomething(Move move, List<Pieces> tempgameboard)
        {
            foreach (var item in tempgameboard)
            {
                if (item.CurrentPosition._PosX == move.endPositions._PosX && item.CurrentPosition._PosY == move.endPositions._PosY)
                    return true;
            }
            return false;
        }
        private void GiveRandomValueToAMove(Move move)
        {
            int nr = rnd.Next(0, 10);
            move.value = nr;
        }
        private bool Threatened(List<Pieces> gameboard, Move move, Pieces piece)
        {
            List<Move> tempmovelist = new List<Move>();
            for (int i = 0; i < gameboard.Count; i++)
            {
                for (int j = 0; j < gameboard[i].ListOfMoves.Count; j++)
                {
                    tempmovelist.Add(gameboard[i].ListOfMoves[j]);
                }
            }
            for (int i = 0; i < gameboard.Count; i++)
            {
                if (piece.PieceColor != gameboard[i].PieceColor)
                {
                    gameboard[i].ListOfMoves.Clear();
                }
            }
            for (int i = 0; i < tempmovelist.Count; i++)
            {
                if (tempmovelist[i].endPositions._PosX == move.endPositions._PosX && tempmovelist[i].endPositions._PosY == move.endPositions._PosY)
                {
                    return true;
                }
            }
            return false;
        }

        private void CanIThreatenTheKing(Move possibleMove, Pieces Piece, List<Pieces> gameboard)
        {
            var saveX = Piece.CurrentPosition._PosX;
            var saveY = Piece.CurrentPosition._PosY;

            var turn = 1;
            if (Piece.PieceColor == Color.Black)
                turn = 2;

            Piece.CurrentPosition._PosX = possibleMove.endPositions._PosX;
            Piece.CurrentPosition._PosY = possibleMove.endPositions._PosY;
            

            if(tempmovelogic.IsEnemyInCheck(turn, gameboard))
            {
                possibleMove.value += 25;
            }
            Piece.CurrentPosition._PosX = saveX;
            Piece.CurrentPosition._PosY = saveY;


        }
       
    }
}
