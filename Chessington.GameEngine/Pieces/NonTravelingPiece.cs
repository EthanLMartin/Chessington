using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessington.GameEngine.Pieces
{
    public abstract class NonTravelingPiece : Piece
    {
        protected NonTravelingPiece(Player player)
            : base(player) { }

        protected List<Square> RemoveFriendlyTakes(List<Square> moves, Board board)
        {
            moves.RemoveAll(s => (board.GetPiece(s) != null) && (board.GetPiece(s).Player == this.Player));
            return moves;
        }
    }
}