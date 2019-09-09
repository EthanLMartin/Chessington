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
            List<Square> moves = new List<Square>();
            Square location = board.FindPiece(this);

            for (int i = 0; i < 8; i++)
            {
                moves.Add(new Square(location.Row + i, location.Col + i));
                moves.Add(new Square(location.Row + i, location.Col - i));
                moves.Add(new Square(location.Row - i, location.Col + i));
                moves.Add(new Square(location.Row - i, location.Col - i));
            }

            // Removes locations outside of the board
            moves.RemoveAll(s => (s.Row >= 8) || (s.Row < 0) || (s.Col >= 8) || (s.Col < 0));

            // Remove starting location
            moves.RemoveAll(s => s == location);
            
            return moves;
        }
    }
}