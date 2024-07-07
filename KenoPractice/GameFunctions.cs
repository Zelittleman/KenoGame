using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenoPractice
{
    public class GameFunctions : MainWindow
    {
        private static readonly Random random = new Random();


        //Generates the random numbers for the drawing
        public List<int> GenerateUniqueRandomNumbers()
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

        //Grab the numbers the user entered in the text box
        public List<int> GetUserNumers(MainWindow form)
        {
            string input = form.UserNumbers.Text;
            string[] parts = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> numbers = new List<int>();

            foreach (string part in parts)
            {

                if (int.TryParse(part.Trim(), out int number))
                {
                    if(number < 1 || number > 80)
                    {
                        throw new FormatException($"'{number}' is not a valid number. Please enter a number between 1 and 80.");
                    } else
                    {
                        numbers.Add(number);
                    }
                    
                }
                else
                {
                    throw new FormatException($"'{part}' is not a valid integer.");
                }
            }

            return numbers;
        }
    }
}
