using System;

namespace Chess.Domain.Models
{
    public class Position
    {
        public readonly int Row;
        public readonly int Column;

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool IsValid()
        {
            return Row <= 7 && Row >= 0 && Column <= 7 && Column >= 0;
        }

        public override string ToString()
        {
            return $"{Row}{Column}";
        }

        public override bool Equals(object other)
        {
            if (!(other is Position otherPosition))
            {
                return false;
            }

            return Row == otherPosition.Row && Column == otherPosition.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static Position operator+ (Position x, Position y)
        {
            var row = x.Row + y.Row;
            var column = x.Column + y.Column;
            return new Position(row, column);
        }

        public static Position FromString(string stringPosition)
        {
            var row = int.Parse(stringPosition[0].ToString());
            var column = int.Parse(stringPosition[1].ToString());
            return new Position(row, column);
        }

        public static Position Clone(Position position)
        {
            return new Position(position.Row, position.Column);
        }
    }
}
