using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : NonTravelingPiece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);
            List<Square> moves = new List<Square>();

            moves.AddRange(GetAdjacentMoves(location));
            moves.AddRange(GetCastlingMoves(location, board));

            moves = board.ClipToBoard(moves);
            moves = RemoveFriendlyTakes(moves, board);

            return moves;
        }

        public override void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            int colDirection = (newSquare - currentSquare).ColDirection;

            Turn turn = new Turn(this, currentSquare, newSquare);

            if (Math.Abs(colDirection) > 1)
            {
                turn += MoveCastleRook(board, currentSquare, colDirection);
            }

            board.MovePiece(currentSquare, newSquare);

            board.GameTurns.Add(turn);
        }

        private List<Square> GetAdjacentMoves(Square location)
        {
            List<Square> moves = new List<Square>
            {
                location + new Direction(1, 1),
                location + new Direction(1, -1),
                location + new Direction(-1, 1),
                location + new Direction(-1, -1),
                location + new Direction(0, 1),
                location + new Direction(0, -1),
                location + new Direction(1, 0),
                location + new Direction(-1, 0)
            };

            return moves;
        }

        private List<Square> GetCastlingMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            if (board.HasMoved(this))
            {
                return moves;
            }

            Direction left = new Direction(0, -1);
            Direction right = new Direction(0, 1);

            Piece piece = board.FindPieceFrom(this, left);
            if (piece is Rook && !board.HasMoved(piece))
            {
                moves.Add(location + left + left);
            }

            piece = board.FindPieceFrom(this, right);
            if (piece is Rook && !board.HasMoved(piece))
            {
                moves.Add(location + right + right);
            }

            return moves;
        }

        private Movement MoveCastleRook(Board board, Square kingSquare, int colDirection)
        {
            Direction direction;
            if (colDirection < 0)
            {
                direction = new Direction(0, -1);
            }
            else
            {
                direction = new Direction(0, 1);
            }

            Piece rook = board.FindPieceFrom(board.GetPiece(kingSquare), direction);

            Movement movement = new Movement(rook, board.FindPiece(rook), kingSquare + direction);

            board.ForceMovePiece(rook, kingSquare + direction);

            return movement;
        }
    }
}