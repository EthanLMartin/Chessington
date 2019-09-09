using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square location = board.FindPiece(this);

            moves.AddRange(GetVerticalMovement(location, board));
            moves.AddRange(GetHorizontalMovement(location, board));

            moves = ClipToBoard(moves, board);

            return moves;
        }

        private List<Square> GetVerticalMovement(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            int offset = 1;
            while (board.IsWithinBounds(new Square(location.Row + offset, location.Col)))
            {
                moves.Add(new Square(location.Row + offset, location.Col));
                if (!board.IsEmpty(location.Row + offset, location.Col))
                {
                    break;
                }
                offset++;
            }

            offset = -1;
            while (board.IsWithinBounds(new Square(location.Row + offset, location.Col)))
            {
                moves.Add(new Square(location.Row + offset, location.Col));
                if (!board.IsEmpty(location.Row + offset, location.Col))
                {
                    break;
                }
                offset--;
            }

            return moves;
        }

        private List<Square> GetHorizontalMovement(Square location, Board board)
        {
            List<Square> moves = new List<Square>();

            int offset = 1;
            while (board.IsWithinBounds(new Square(location.Row, location.Col + offset)))
            {
                moves.Add(new Square(location.Row, location.Col + offset));
                if (!board.IsEmpty(location.Row, location.Col + offset))
                {
                    break;
                }
                offset++;
            }

            offset = -1;
            while (board.IsWithinBounds(new Square(location.Row, location.Col + offset)))
            {
                moves.Add(new Square(location.Row, location.Col + offset));
                if (!board.IsEmpty(location.Row, location.Col + offset))
                {
                    break;
                }
                offset--;
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