using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessGameLogic;

namespace ChessUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImages = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position,Move>moveCache = new Dictionary<Position,Move>();  
        private GameState gameState;
        private Position selectedPos = null;
        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
            gameState = new GameState(Player.white, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPalyer);
        }
        public void InitializeBoard()
        {
            const int GridLimit = 8;
            for(int i = 0; i < GridLimit; i++)
            {
                for(int j=0;j< GridLimit; j++)
                {
                    Image image = new Image();
                    pieceImages[i,j] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight = new Rectangle();
                    highlights[i, j] = highlight;
                    HighlightGrid.Children.Add(highlight);  
                }
            }
        }
        private void DrawBoard(Board board)
        {
            const int GridLimit = 8;
            for (int i = 0; i < GridLimit; i++)
            {
                for (int j = 0; j < GridLimit; j++)
                {
                    Piece piece = board[i, j];
                    pieceImages[i,j].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(IsMenuOnScreen()) {
                return;            
            }
            Point point = e.GetPosition(BoardGrid);
            Position pos = ToSquarePosition(point);
            if(selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPos = null;
            HideHightlights();  
            if(moveCache.TryGetValue(pos,out Move move))
            {
                if (move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                {
                    HandleMove(move);
                }
            }
        }
        private void HandlePromotion(Position from,Position to)
        {
            pieceImages[to.Row, to.Column].Source = Images.GetImage(gameState.CurrentPalyer, PieceType.Pawn);
            pieceImages[from.Row, from.Column].Source = null;
            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentPalyer);
            MenuContainer.Content = promMenu;
            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move PromMove = new PawnPromotions(from, to, type);
                HandleMove(PromMove);
            };
        }
        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPalyer);

            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves = gameState.LegalMovesForPiece(pos);
            if (moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighLights();
            }
        }
        private Position ToSquarePosition (Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Position ( row, col );
        }
        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();
            foreach(Move move in moves) {
                moveCache[move.ToPos] = move;
            }
        }
        private void ShowHighLights()
        {
            Color color = Color.FromArgb(255,10,100,100);
            foreach(var to in moveCache.Keys)
            {
                highlights[to.Row,to.Column].Fill = new SolidColorBrush(color);
            }
        }
        private void HideHightlights()
        {
            foreach (var to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }
        private void SetCursor (Player player)
        {
            if(player == Player.white)
            {
                Cursor = ChessCursors.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursors.BlackCursor;
            }
        }
        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }
        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }
        private void RestartGame()
        {
            selectedPos = null;
            HideHightlights();
            moveCache.Clear();
            gameState = new GameState(Player.white, Board.Initial());
            DrawBoard(gameState.Board);
            SetCursor(gameState.CurrentPalyer);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(!IsMenuOnScreen()&&e.Key == Key.Escape)
            {
                ShowPauseMenu();
            }
        }
        private void ShowPauseMenu()
        {
            PauseMenu pauseMenu = new PauseMenu();
            MenuContainer.Content = pauseMenu;
            pauseMenu.OptionSelected += option =>
            {
                MenuContainer.Content = null;//hide the menu
                if(option == Option.Restart)
                {
                    RestartGame();
                }
            };
        }
    }

}