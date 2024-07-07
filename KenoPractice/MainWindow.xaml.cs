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
            startBoardReset();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startBoardReset();
            playFunctions.runUntilWin(this);
        }


        private void singleRun_Click(object sender, RoutedEventArgs e)
        {
            startBoardReset();
            playFunctions.SingleRun(this);
        }

        //reset the board
        private void startBoardReset()
        {
            int count = 0;
            string formattedText = " ";

            for (int x = 1; x <= 80; x++)
            {
                if (x < 9)
                {
                    formattedText += x.ToString() + "    ";
                    count++;
                }
                else if (x == 9)
                {
                    formattedText += x.ToString() + "   ";
                    count++;
                }
                else
                {
                    formattedText += x.ToString() + "  ";
                    count++;
                }


                if (count % 10 == 0)
                {
                    formattedText = formattedText.TrimEnd() + Environment.NewLine;
                }

                if (count == 40)
                {
                    formattedText = formattedText + Environment.NewLine;
                }
            }
            numberBoard.Document.Blocks.Clear();
            numberBoard.Document.Blocks.Add(new Paragraph(new Run(formattedText)));


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            startBoardReset();
            playFunctions.playVertsTilWin(this);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            startBoardReset();
            playFunctions.playBoxesTilWin(this);
        }
    }
}
