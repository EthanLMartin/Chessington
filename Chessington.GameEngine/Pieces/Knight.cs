using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            Square location = board.FindPiece(this);
            List<Square> moves = GetLMoves(location);

            moves = ClipToBoard(moves, board);
            moves = RemoveFriendlyTakes(moves, board);

            return moves;
        }

        private List<Square> GetLMoves(Square location)
        {
            List<Square> moves = new List<Square>();

            moves.Add(new Square(location.Row + 1, location.Col + 2));
            moves.Add(new Square(location.Row + 1, location.Col - 2));
            moves.Add(new Square(location.Row - 1, location.Col + 2));
            moves.Add(new Square(location.Row - 1, location.Col - 2));
            moves.Add(new Square(location.Row + 2, location.Col + 1));
            moves.Add(new Square(location.Row + 2, location.Col - 1));
            moves.Add(new Square(location.Row - 2, location.Col + 1));
            moves.Add(new Square(location.Row - 2, location.Col - 1));

            return moves;
        }

        private List<Square> ClipToBoard(List<Square> moves, Board board)
        {
            moves.RemoveAll(s => !board.IsWithinBounds(s));
            return moves;
        }

        private List<Square> RemoveFriendlyTakes(List<Square> moves, Board board)
        {
            moves.RemoveAll(s => (board.GetPiece(s) != null) && (board.GetPiece(s).Player == this.Player));
            return moves;
        }
    }
}