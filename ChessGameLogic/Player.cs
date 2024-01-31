
namespace ChessGameLogic
{
    public enum Player
    {
        //this enum will be used to store which player turn is it and who won the game and the color of the chess peaces
        None ,//draw
        white ,
        Black ,
    }
    static class PlayerExtensions
    {
        public static Player PlayerOpponent(this Player player)
        {
            if (player == Player.white) return Player.Black;
            else if (player == Player.Black) return Player.white;
            else return Player.None;
        }
    }
}
