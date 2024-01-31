
using ChessGameLogic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace ChessUserInterface
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pawn,LoadImage("Assets/PawnW.png") },
            {PieceType.Bishop,LoadImage("Assets/BishopW.png") },
            {PieceType.King,LoadImage("Assets/KingW.png") },
            {PieceType.Queen,LoadImage("Assets/QueenW.png") },
            {PieceType.Rook,LoadImage("Assets/RookW.png") },
            {PieceType.Knight,LoadImage("Assets/KnightW.png") }
        };
        private static readonly Dictionary<PieceType, ImageSource> BlackSources = new()
        {
            {PieceType.Pawn,LoadImage("Assets/PawnB.png") },
            {PieceType.Bishop,LoadImage("Assets/BishopB.png") },
            {PieceType.King,LoadImage("Assets/KingB.png") },
            {PieceType.Queen,LoadImage("Assets/QueenB.png") },
            {PieceType.Rook,LoadImage("Assets/RookB.png") },
            {PieceType.Knight,LoadImage("Assets/KnightB.png") }
        };
        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath,UriKind.Relative));  
        }
        public static ImageSource GetImage(Player color , PieceType type)
        {
            if(color== Player.white)
            {
                return whiteSources[type];

            }
            else if (color == Player.Black)
            {
                return BlackSources[type];
            }
            else
            {
                return null;//not reachable;
            }
        }
        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }
            else
            {
                return GetImage(piece.Color,piece.Type);
            }
        }
    }
}
