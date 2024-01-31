
namespace ChessGameLogic
{
    public class Position
    {
        //this class represent a position of square on the board
        //note the board is like 2D array starting from top left as point (0,0)
        //and the bottom right is (7,7)
        public int Row
        {
            get;
        }
        public int Column
        {
            get;
        }
        //constructor to initilize the 2 fields
        public Position(int row,int column)
        {
            this.Row = row;
            this.Column = column;
        }
        public Player SquareColor()
        {
            if ((Row + Column) % 2 == 0) return Player.white;//obserivation
            return Player.Black;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }
        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Row + dir.RowDetla, pos.Column + dir.ColumnDetla);
        }
    }
}
