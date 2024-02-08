namespace TicTacToeWeb.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string[] Spaces { get; set; } = new string[3];

        public Board()
        {
            Spaces[0] = "___|___|___";
            Spaces[1] = "___|___|___";
            Spaces[2] = "___|___|___";
        }
    }
}