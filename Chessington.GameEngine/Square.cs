namespace Chessington.GameEngine
{
    public struct Square
    {
        public readonly int Row;
        public readonly int Col;

        public Square(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static Square At(int row, int col)
        {
            return new Square(row, col);
        }

        public bool Equals(Square other)
        {
            return Row == other.Row && Col == other.Col;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Square && Equals((Square)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Col;
            }
        }

        public static bool operator ==(Square left, Square right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Square left, Square right)
        {
            return !left.Equals(right);
        }

        public static Square operator +(Square left, Direction right)
        {
            return At(left.Row + right.RowDirection, left.Col + right.ColDirection);
        }

        public static Square operator -(Square left, Direction right)
        {
            return At(left.Row - right.RowDirection, left.Col - right.ColDirection);
        }

        public static Direction operator -(Square left, Square right)
        {
            return new Direction(left.Row - right.Row, left.Col - right.Col);
        }

        public override string ToString()
        {
            return string.Format("Row {0}, Col {1}", Row, Col);
        }
    }

    public struct Direction
    {
        public readonly int RowDirection;
        public readonly int ColDirection;

        public Direction(int rowDirection, int colDirection)
        {
            RowDirection = rowDirection;
            ColDirection = colDirection;
        }

        public static Direction operator +(Direction left, Direction right)
        {
            return new Direction(left.RowDirection + right.RowDirection, left.ColDirection + right.ColDirection);
        }
    }
}