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

namespace KenoPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                    bigWin = allWinningResults(results, userNumbers);
                     
                }

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

        private bool allWinningResults(List<int> picks, List<int> userNumbers)
        {
            int matches = userNumbers.Count(userNumber => picks.Contains(userNumber));
            return matches == userNumbers.Count;

        }

        private void UpdateResultList(List<int> results)
        {
            ResultsLabel.Content = string.Join(", ", results);
        }
    }
}
