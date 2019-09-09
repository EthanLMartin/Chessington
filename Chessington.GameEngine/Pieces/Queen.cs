using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            List<Square> moves = new List<Square>();
            Square location = board.FindPiece(this);

            moves.AddRange(GetDiagonalMoves(location));
            moves.AddRange(GetLateralMoves(location));

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

        private List<Square> GetLateralMoves(Square location)
        {
            List<Square> moves = new List<Square>();

            for (int row = 0; row < GameSettings.BoardSize; row++)
            {
                moves.Add(new Square(row, location.Col));
            }

            for (int col = 0; col < GameSettings.BoardSize; col++)
            {
                moves.Add(new Square(location.Row, col));
            }

            moves.RemoveAll(s => s == location);

            return moves;
        }

        private List<Square> ClipToBoard(List<Square> moves, Board board)
        {
            moves.RemoveAll(s => !board.IsWithinBounds(s));
            return moves;
        }
    }
}