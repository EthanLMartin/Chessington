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
            List<Square> moves = GetDiagonalMoves(location);
            moves = ClipToBoard(moves, board);

            return moves;
        }

        private List<Square> GetDiagonalMoves(Square location)
        {
            List<Square> moves = new List<Square>();

            for (int i = 1; i < GameSettings.BoardSize; i++)
            {
                moves.Add(new Square(location.Row + i, location.Col + i));
                moves.Add(new Square(location.Row + i, location.Col - i));
                moves.Add(new Square(location.Row - i, location.Col + i));
                moves.Add(new Square(location.Row - i, location.Col - i));
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