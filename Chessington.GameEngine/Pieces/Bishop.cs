using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);
            List<Square> moves = new List<Square>();

            moves.AddRange(GetDiagonalNEMoves(location, board));
            moves.AddRange(GetDiagonalSEMoves(location, board));
            moves.AddRange(GetDiagonalNWMoves(location, board));
            moves.AddRange(GetDiagonalSWMoves(location, board));

            moves = ClipToBoard(moves, board);

            return moves;
        }

        private List<Square> GetDiagonalSEMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            for (int i = 1; i < GameSettings.BoardSize; i++)
            {
                Square loc = new Square(location.Row - i, location.Col + i);
                moves.Add(loc);
                if (!board.IsWithinBounds(loc) || !board.IsEmpty(location.Row - i, location.Col + i))
                {
                    break;
                }
            }

            return moves;
        }

        private List<Square> GetDiagonalNEMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            for (int i = 1; i < GameSettings.BoardSize; i++)
            {
                Square loc = new Square(location.Row + i, location.Col + i);
                moves.Add(loc);
                if (!board.IsWithinBounds(loc) || !board.IsEmpty(location.Row + i, location.Col + i))
                {
                    break;
                }
            }

            return moves;
        }

        private List<Square> GetDiagonalSWMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            for (int i = 1; i < GameSettings.BoardSize; i++)
            {
                Square loc = new Square(location.Row + i, location.Col - i);
                moves.Add(loc);
                if (!board.IsWithinBounds(loc) || !board.IsEmpty(location.Row + i, location.Col - i))
                {
                    break;
                }
            }

            return moves;
        }

        private List<Square> GetDiagonalNWMoves(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            for (int i = 1; i < GameSettings.BoardSize; i++)
            {
                Square loc = new Square(location.Row - i, location.Col - i);
                moves.Add(loc);
                if (!board.IsWithinBounds(loc) || !board.IsEmpty(location.Row - i, location.Col - i))
                {
                    break;
                }
            }

            return moves;
        }

        private List<Square> ClipToBoard(List<Square> moves, Board board)
        {
            moves.RemoveAll(s => !board.IsWithinBounds(s));
            return moves;
        }
    }
}