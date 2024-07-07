using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KenoPractice
{
    public class PlayFunctions : MainWindow
    {
        GameFunctions gameFunctions = new GameFunctions();
        ResultFunctions resultFunctions = new ResultFunctions();
        
        //runs the game until the user wins
        public void runUntilWin(MainWindow form)
        {

            try
            {
                int tries = 0;
                bool bigWin = false;

                List<int> userNumbers = gameFunctions.GetUserNumers(form);
                List<int> results = new List<int> { };
                if (userNumbers.Count <= 0)
                {
                    throw new FormatException($"Please enter at least one number ");
                }

                while (!bigWin)
                {
                    tries++;
                    results = gameFunctions.GenerateUniqueRandomNumbers();
                    results.Sort();
                    resultFunctions.UpdateResultList(results, form);
                    bigWin = ((userNumbers.Count(userNumber => results.Contains(userNumber))) == userNumbers.Count());
                        
                }

                resultFunctions.HighlightNumbers(results, form);

                MessageBox.Show($"It took {tries} tries to hit your numbers");

            }

            catch (Exception ex)
            {
                MessageBox.Show("Invalid input: " + ex.Message);
            }


        }

        private (bool win, int count) SingleResults(List<int> results, List<int> userNumbers)
        {
            int matches = (userNumbers.Count(userNumber => results.Contains(userNumber)));

            return (matches == userNumbers.Count, matches);
        }

        //plays a single game with the numbers the user has chosen
        public void SingleRun(MainWindow form)
        {
            
            try
            {
                List<int> userNumbers = gameFunctions.GetUserNumers(form);
                List<int> results = new List<int> { };
                int pickCount = userNumbers.Count;
                bool Win = false;
                int hits = 0;

                if (userNumbers.Count <= 0)
                {
                    throw new FormatException($"Please enter at least one number ");
                }

                results = gameFunctions.GenerateUniqueRandomNumbers();
                results.Sort();
                resultFunctions.UpdateResultList(results,form);
                (Win, hits) = SingleResults(results, userNumbers);
                resultFunctions.HighlightNumbers(results,form);

                if (Win)
                {
                    MessageBox.Show($"Big winner! You matched all your numbers!");
                }
                else if (hits > 0)
                {
                    MessageBox.Show($"You matched {hits} out of your {pickCount} numbers.");
                }
                else
                {
                    MessageBox.Show($"You didn't match any numbers.");
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("Invalid input: " + ex.Message);
            }
        }

        //plays the game until the user hits 2 verts
        public void playVertsTilWin (MainWindow form)
        {
            List<int> results = new List<int> { };
            bool Win = false; 
            int count = 0;
            int plays = 0;

            //run games until the user hits 2 verts
            while(!Win)
            {
                //how many vert hits the user got in the game
                int hits = 0;
                //the current top number to check the vert
                int vertPointer = 1;

                //generate the results and sort them
                results = gameFunctions.GenerateUniqueRandomNumbers();
                results.Sort();

                //cycle through all the verts
                while (vertPointer <= 50)
                {
                    //run through the vert
                    for (int x = 0; x <= 3; x++)
                    {
                        int currentNumber = vertPointer + (x * 10);
                        if (results.Contains(currentNumber))
                        {
                            //if the number is in the results, increment the count
                            count++;
                            if (count == 4)
                            {
                                //if all 4 in the vert are in the results, increment the hits
                                hits++;
                            }

                        }
                    }

                    //reset the count and move to the next vert
                    count = 0;
                    vertPointer++;

                    //once we hit the last top row vert, move to the bottom row
                    if (vertPointer == 11)
                    {
                        vertPointer = 41;
                    }

                    //check if the user has hit 2 verts in the game
                    if (hits >= 2)
                    {
                        Win = true;
                        break;
                    }
                }
                plays++;
            }

            resultFunctions.UpdateResultList(results, form);
            resultFunctions.HighlightNumbers(results, form);
            MessageBox.Show($"It took {plays} plays to hit 2 verts");

        }

        public void playBoxesTilWin(MainWindow form)
        {
            List<int> results = new List<int> { };
            bool Win = false;
            int count = 0;
            int plays = 0;


            while (!Win)
            {
                //how many box hits the user got in the game
                int hits = 0;
                //the current top number to check the box
                int boxPointer = 1;

                //generate the results and sort them
                results = gameFunctions.GenerateUniqueRandomNumbers();
                results.Sort();


                while (boxPointer <= 69)
                {
                    //Run through boxes given the pointer  Ex: 1 -- 2 -- 11 -- 12
                    for (int y = 0; y < 2; y++)
                    {
                        for (int x = 0; x <= 1; x++)
                        {

                            int currentNumber = boxPointer + (x * 10);
                            if (results.Contains(currentNumber))
                            {
                                
                                count++;
                                if (count == 4)
                                {
                                    hits++;     
                                }
                            }

                        }
                        
                        //Need to skip over 
                        if (boxPointer == 10 || boxPointer == 30 || boxPointer == 50)
                        {
                            boxPointer+= 10;
                        }

                        boxPointer++;
                    }



                    //reset the count and move to the next box
                    count = 0;

                    if (hits >= 2)
                    {
                        Win = true;
                        break;
                    }
                }
               plays++;
            }

            resultFunctions.UpdateResultList(results, form);
            resultFunctions.HighlightNumbers(results, form);
            MessageBox.Show($"It took {plays} plays to hit 2 boxes");
        }
    }
}
