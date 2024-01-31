using ChessGameLogic;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
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

namespace ChessUserInterface
{
    /// <summary>
    /// Interaction logic for GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;
        public GameOverMenu(GameState gamestate)
        {
            InitializeComponent();

            Result result = gamestate.Result;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reason, gamestate.CurrentPalyer);
        }
        private static string GetWinnerText(Player winner)
        {
            if(winner == Player.white)
            {
                return "WHITE WIN";
            }
            else if(winner == Player.Black)
            {
                return "Black WIN";
            }
            return "IT's A Draw";
        }
        private static string PlayerString (Player player)
        {
            if (player == Player.white)
            {
                return "WHITE";
            }
            else if (player == Player.Black)
            {
                return "Black";
            }
            return "";
        }
        private static string GetReasonText(EndReason reason,Player CurrentPlayer)
        {
            if(reason == EndReason.Stalemate)
            {
                return $"STALEMATE - {PlayerString(CurrentPlayer)} CAN'T MOVE";
            }
            if(reason == EndReason.Checkmate)
            {
                return $"CHECKMATE - {PlayerString(CurrentPlayer)} CAN'T MOVE";
            }
            if (reason == EndReason.FiftyMoveRule)
            {
                return $"FIFTY-MOVE RULE";
            }
            if(reason == EndReason.InsufficientMaterial)
            {
                return "INSUFFICIENTMATERIAL";
            }
            return "THREEFOLD REPETITION";
        }
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }

    }
}
