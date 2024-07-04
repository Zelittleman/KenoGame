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
        private static readonly Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            startBoardReset();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            startBoardReset();
            try
            {
                int tries = 0;
                bool bigWin = false;

                List<int> userNumbers = GetUserNumers();
                List<int> results = new List<int> { };
                if (userNumbers.Count <= 0)
                {
                    throw new FormatException($"Please enter at least one number ");
                }

                while (!bigWin)
                {                    
                    tries++; 
                    results = GenerateUniqueRandomNumbers();                
                    results.Sort();
                    UpdateResultList(results);
                    bigWin = AllWinningResults(results, userNumbers);                  
                }

                HighlightNumbers(results);

                MessageBox.Show($"It took {tries} tries to hit your numbers");

            }

            catch (Exception ex)
            {
                MessageBox.Show("Invalid input: " + ex.Message);
            }


        }

        public static List<int> GenerateUniqueRandomNumbers()
        {

            HashSet<int> numbersSet = new HashSet<int>();

            while (numbersSet.Count < 20)
            {
                int number = random.Next(1, 81); // Generates a number between 1 and 80 (inclusive)
                numbersSet.Add(number); // HashSet automatically handles duplicates
            }


            List<int> numbersList = new List<int>(numbersSet);
            return numbersList;
        }

        private List<int> GetUserNumers()
        {
            string input = UserNumbers.Text;
            string[] parts = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();

            foreach (string part in parts) 
            {
                if (int.TryParse(part.Trim(), out int number))
                {
                    numbers.Add(number);
                }
                else
                {
                    throw new FormatException($"'{part}' is not a valid integer.");
                }
            }

            return numbers;
        }

        private bool AllWinningResults(List<int> picks, List<int> userNumbers)
        {
            int matches = userNumbers.Count(userNumber => picks.Contains(userNumber));
            return matches == userNumbers.Count;
        }

        private (bool win,int count) SingleResults(List<int> picks, List<int> userNumbers)
        {
            int matches = userNumbers.Count(userNumber => picks.Contains(userNumber));

            return (matches == userNumbers.Count,matches);
        }

        private void UpdateResultList(List<int> results)
        {
            ResultsLabel.Content = string.Join(", ", results);
        }

        private void startBoardReset()
        {
            int count = 0;
            string formattedText = " ";

            for(int x = 1;  x<= 80; x++)
            {
                if(x < 9)
                {
                    formattedText += x.ToString() + "    ";
                    count++;
                } else if (x == 9)
                {
                    formattedText += x.ToString() + "   ";
                    count++;
                } else
                {
                    formattedText += x.ToString() + "  ";
                    count++;
                }
                

                if (count % 10 == 0)
                {
                    formattedText = formattedText.TrimEnd() + Environment.NewLine;
                }

                if (count == 40 )
                {
                    formattedText = formattedText + Environment.NewLine;
                }
            }
            numberBoard.Document.Blocks.Clear();
            numberBoard.Document.Blocks.Add(new Paragraph(new Run(formattedText)));

            
        }

        private void HighlightNumbers(List<int> selectedNumbers)
        {
            TextPointer navigator = numberBoard.Document.ContentStart;

            while (navigator.CompareTo(numberBoard.Document.ContentEnd) < 0)
            {
                
                if (navigator.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    string textRun = navigator.GetTextInRun(LogicalDirection.Forward);

                    foreach (var number in selectedNumbers.ToList())
                    {
                        string numberStr = number.ToString();                 

                        int index = textRun.IndexOf(numberStr);
                        if (index != -1)
                        {
                            
                            TextPointer start = navigator.GetPositionAtOffset(index);
                            TextPointer end = start.GetPositionAtOffset(numberStr.Length);
                            TextRange textRange = new TextRange(start, end);
                            textRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Red);
                            selectedNumbers.Remove(number);
                            break;
                        }

                    }

                    

                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        private void singleRun_Click(object sender, RoutedEventArgs e)
        {
            startBoardReset();
            try
            {
                List<int> userNumbers = GetUserNumers();
                List<int> results = new List<int> { };
                int pickCount = userNumbers.Count;
                bool Win = false;
                int hits = 0;

                if (userNumbers.Count <= 0)
                {
                    throw new FormatException($"Please enter at least one number ");
                }

                results = GenerateUniqueRandomNumbers();
                results.Sort();
                UpdateResultList(results);
                (Win, hits) = SingleResults(results, userNumbers);
                HighlightNumbers(results);

                if (Win)
                {
                  MessageBox.Show($"Big winner! You matched all your numbers!");
                } else if(hits > 0)
                {
                  MessageBox.Show($"You matched {hits} out of your {pickCount} numbers.");
                } else
                {
                  MessageBox.Show($"You didn't match any numbers.");
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("Invalid input: " + ex.Message);
            }
        }
    }
}
