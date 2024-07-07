using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace KenoPractice
{
    public class ResultFunctions : MainWindow
    {
       


        //update the label containing the results of the drawing
        public void UpdateResultList(List<int> results,MainWindow form)
        {
            form.ResultsLabel.Content = string.Join(", ", results);
        }


        //Highlights the numbers picked in the drawing
        public void HighlightNumbers(List<int> selectedNumbers, MainWindow form)
        {
            TextPointer navigator = form.numberBoard.Document.ContentStart;

            while (navigator.CompareTo(form.numberBoard.Document.ContentEnd) < 0)
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

        //reset the board
        public void startBoardReset(MainWindow form)
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
            form.numberBoard.Document.Blocks.Clear();
            form.numberBoard.Document.Blocks.Add(new Paragraph(new Run(formattedText)));
        }
    }
}
