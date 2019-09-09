using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : NonTravelingPiece
    {
        public Pawn(Player player) 
            : base(player) { }
        
        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);

            switch (this.Player)
            {
                case Player.White:
                    return GetMoves(location, board, new Direction(-1, 0));

                case Player.Black:
                    return GetMoves(location, board, new Direction(1, 0));

                default:
                    return new List<Square>();
            }
        }

        private List<Square> GetMoves(Square location, Board board, Direction direction)
        {
            List<Square> moves = new List<Square>();

            moves.AddRange(CheckForwardMoves(location, board, direction));
            moves.AddRange(CheckDiagonalMoves(location, board, direction));
            moves.AddRange(CheckEnPassantMoves(location, board, direction));

            return moves;
        }

        private List<Square> CheckForwardMoves(Square location, Board board, Direction direction)
        {
            List<Square> moves = new List<Square>();
            Square moveLocation = location + direction;

            if (board.IsWithinBounds(moveLocation) && board.IsEmpty(moveLocation))
            {
                moves.Add(moveLocation);
            }
            else
            {
                return moves;
            }

            moveLocation += direction;
            if (!HasMoved && board.IsWithinBounds(moveLocation) && board.IsEmpty(moveLocation))
            {
                moves.Add(moveLocation);
            }

            return moves;
        }

        private List<Square> CheckDiagonalMoves(Square location, Board board, Direction direction)
        {
            Square leftDiagonal = location + direction + new Direction(0, -1);
            Square rightDiagonal = location + direction + new Direction(0, 1);

            List<Square> moves = new List<Square>();

            if (board.IsWithinBounds(leftDiagonal) && !board.IsEmpty(leftDiagonal))
            {
                moves.Add(leftDiagonal);
            }

            if (board.IsWithinBounds(rightDiagonal) && !board.IsEmpty(rightDiagonal))
            {
                moves.Add(rightDiagonal);
            }

            moves = RemoveFriendlyTakes(moves, board);

            return moves;
        }

        private List<Square> CheckEnPassantMoves(Square location, Board board, Direction direction)
        {
            Square leftDiagonal = location + direction + new Direction(0, -1);
            Square rightDiagonal = location + direction + new Direction(0, 1);

            List<Square> moves = new List<Square>();

            List<Move> enemyMoves = board.GetEnemyMoves(this.Player);

            if (enemyMoves.Count == 0)
            {
                return moves;
            }

            Move lastEnemyMove = enemyMoves[enemyMoves.Count - 1];

            if (!(lastEnemyMove.MovementPiece is Pawn))
            {
                return moves;
            }

            if (Math.Abs((lastEnemyMove.SquareTo - lastEnemyMove.SquareFrom).RowDirection) == 1)
            {
                return moves;
            }

            if (board.IsWithinBounds(leftDiagonal) && board.IsEmpty(leftDiagonal))
            {
                if (lastEnemyMove.SquareTo == (leftDiagonal - direction))
                {
                    moves.Add(leftDiagonal);
                }
            }

            if (board.IsWithinBounds(rightDiagonal) && board.IsEmpty(rightDiagonal))
            {
                if (lastEnemyMove.SquareTo == (rightDiagonal - direction))
                {
                    moves.Add(rightDiagonal);
                }
            }

            return moves;
        }
    }
}