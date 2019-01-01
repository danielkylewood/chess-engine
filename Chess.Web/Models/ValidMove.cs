using System.Collections.Generic;

namespace Chess.Web.Models
{
    public class ValidMove
    {
        public string PiecePosition { get; set; }
        public List<string> PieceMoves { get; set; }
    }
}
