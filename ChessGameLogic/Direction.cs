
namespace ChessGameLogic
{
    public class Direction
    {
        //north , south , East , West , North East , North west , South east , South west
        public static readonly int[] _dx = { -1, 1, 0, 0,-1, -1, 1, 1 };
        public static readonly int[] _dy = { 0, 0, 1, -1, 1, -1, 1, -1 };
        public static readonly Direction North = new Direction(_dx[0], _dy[0]);
        public static readonly Direction South = new Direction(_dx[1], _dy[1]);
        public static readonly Direction East = new Direction(_dx[2], _dy[2]);
        public static readonly Direction West = new Direction(_dx[3], _dy[3]);
        public static readonly Direction NorthEast = new Direction(_dx[4], _dy[4]);
        public static readonly Direction NorthWest = new Direction(_dx[5], _dy[5]);
        public static readonly Direction Southeast = new Direction(_dx[6], _dy[6]);
        public static readonly Direction Southwest = new Direction(_dx[7], _dy[7]);        
        public int RowDetla { get; set; }
        public int ColumnDetla { get; set; }

        public Direction(int RD, int CD)
        {
            RowDetla = RD;
            ColumnDetla = CD;
        }
        public static Direction operator+(Direction lhs, Direction rhs)
        {
            return new Direction(lhs.RowDetla + rhs.RowDetla, lhs.ColumnDetla + rhs.ColumnDetla);
        }
        public static Direction operator *(int Scale, Direction rhs)
        {
            return new Direction(Scale * rhs.RowDetla, Scale * rhs.ColumnDetla);
        }

    }
}
