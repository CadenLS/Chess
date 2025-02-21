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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private GameTypes selectedGameType;
        public Menu()
        {
            InitializeComponent();
        }

        private void Chess960_Click(object sender, RoutedEventArgs e)
        {
            selectedGameType = GameTypes.Chess960;
            OpenMainWindow();
        }

        private void Chess_click(object sender, RoutedEventArgs e)
        {
            selectedGameType = GameTypes.Chess;
            OpenMainWindow();
        }

        private void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow(selectedGameType);
            mainWindow.Show();
            this.Close();
        }

    }
}
