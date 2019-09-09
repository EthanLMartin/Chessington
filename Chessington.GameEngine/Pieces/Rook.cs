using System.Collections.Generic;
using System.Linq;

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

            for (int row = 0; row < 8; row++)
            {
                moves.Add(new Square(row, location.Col));
            }

            for (int col = 0; col < 8; col++)
            {
                moves.Add(new Square(location.Row, col));
            }

            // Remove starting location
            moves.RemoveAll(s => s == location);

            return moves;
        }
    }
}