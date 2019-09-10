using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Turn
    {
        public readonly Piece MainPiece;
        public readonly List<Movement> Moves;
        public readonly Player Player;

        public Turn(Piece piece, List<Movement> move, Player player)
        {
            MainPiece = piece;
            Moves = move;
            Player = player;
        }
        public Turn(Piece piece, Square squareFrom, Square squareTo)
        {
            MainPiece = piece;
            Moves = new List<Movement>{ new Movement(piece, squareFrom, squareTo) };
            Player = MainPiece.Player;
        }

        public int PiecesInTurn()
        {
            return Moves.Count;
        }

        public static Turn operator +(Turn left, Movement right)
        {
            return new Turn(
                left.MainPiece,
                left.Moves.Concat(new List<Movement>{right}).ToList(),
                left.Player
            );
        }
    }

    public struct Movement
    {
        public readonly Piece MovementPiece;
        public readonly Square SquareFrom;
        public readonly Square SquareTo;

        public Movement(Piece piece, Square squareFrom, Square squareTo)
        {
            MovementPiece = piece;
            SquareFrom = squareFrom;
            SquareTo = squareTo;
        }

        public static Turn operator +(Movement left, Movement right)
        {
            return new Turn(
                left.MovementPiece, 
                new List<Movement>{left, right},
                left.MovementPiece.Player
                );
        }
    }


}
