namespace Chess.Domain.Models
{
    public class Square
    {
        public Position Position;
        public string Colour;
        public string Id => $"{Position.Row}{Position.Column}";

        public Square(int row, int column)
        {
            Position = new Position(row, column);
        }
    }
}
