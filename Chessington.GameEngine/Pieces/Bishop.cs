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
            List<Square> moves = GetAllDiagonals(location, GameSettings.BoardSize);
            moves.RemoveAll(s => !board.IsWithinBounds(s));

            return moves;
        }

        private List<Square> GetAllDiagonals(Square location, int range)
        {
            List<Square> moves = new List<Square>();

            for (int i = 1; i < range; i++)
            {
                moves.Add(new Square(location.Row + i, location.Col + i));
                moves.Add(new Square(location.Row + i, location.Col - i));
                moves.Add(new Square(location.Row - i, location.Col + i));
                moves.Add(new Square(location.Row - i, location.Col - i));
            }

            return moves;
        }
    }
}