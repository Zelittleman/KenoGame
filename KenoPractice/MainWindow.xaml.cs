using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;

namespace KenoPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        
        static readonly GameFunctions gameFunctions = new GameFunctions();
        static readonly ResultFunctions resultFunctions = new ResultFunctions();
        static readonly PlayFunctions playFunctions = new PlayFunctions();

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Now it's safe to call startBoardReset
            resultFunctions.startBoardReset(this);
        }

        private void singleRun_Click(object sender, RoutedEventArgs e)
        {
            resultFunctions.startBoardReset(this);
            playFunctions.SingleRun(this);
        }

        private void Verts_Click(object sender, RoutedEventArgs e)
        {
            resultFunctions.startBoardReset(this);
            playFunctions.playVertsTilWin(this);
        }

        private void Boxes_Click(object sender, RoutedEventArgs e)
        {
            resultFunctions.startBoardReset(this);
            playFunctions.playBoxesTilWin(this);
        }

        private void RunTilWin_Click(object sender, RoutedEventArgs e)
        {
            resultFunctions.startBoardReset(this);
            playFunctions.runUntilWin(this);
        }

    }
}
