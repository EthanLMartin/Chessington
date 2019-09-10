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

        public override void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);

            if (currentSquare.Col != newSquare.Col)
            {
                Direction direction;

                if (currentSquare.Col < newSquare.Col)
                {
                    direction = new Direction(0, 1);
                }
                else
                {
                    direction = new Direction(0, -1);
                }

                board.RemoveAt(currentSquare + direction);
            }

            board.GameTurns.Add(new Turn(this, currentSquare, newSquare));
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
            if (!board.HasMoved(this) && board.IsWithinBounds(moveLocation) && board.IsEmpty(moveLocation))
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

        private IEnumerable<Square> CheckEnPassantMoves(Square location, Board board, Direction direction)
        {
            Square leftDiagonal = location + direction + new Direction(0, -1);
            Square rightDiagonal = location + direction + new Direction(0, 1);
            
            Turn lastEnemyTurn = board.GetEnemyTurns(Player).LastOrDefault();

            if (!IsPawnDoubleAdvanceMove(lastEnemyTurn))
            {
                yield break;
            }

            if (board.IsWithinBounds(leftDiagonal) && board.IsEmpty(leftDiagonal))
            {
                if (lastEnemyTurn.Moves.First().SquareTo == (leftDiagonal - direction))
                {
                    yield return leftDiagonal;
                }
            }

            if (board.IsWithinBounds(rightDiagonal) && board.IsEmpty(rightDiagonal))
            {
                if (lastEnemyTurn.Moves.First().SquareTo == (rightDiagonal - direction))
                {
                    yield return rightDiagonal;
                }
            }
        }

        private static bool IsPawnDoubleAdvanceMove(Turn turn)
        {
            return turn?.MainPiece is Pawn && 
                   turn.PiecesInTurn() == 1 &&
                   Math.Abs((turn.Moves.First().SquareTo - turn.Moves.First().SquareFrom).RowDirection) > 1;
        }
    }
}