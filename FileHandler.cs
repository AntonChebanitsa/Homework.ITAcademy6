using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework.ITAcademy6
{
    public class FileHandler
    {
        private const string PATH_TO_START_FILE = "D:\\sample.txt";

        public string FileReader()
        {
            var reader = new StreamReader(PATH_TO_START_FILE);
            return reader.ReadToEnd();
        }

        public string[] SplitBySentences()
        {
            var text = FileReader();
            var sentences = Regex.Split(text, "[!.?;\\t\\r\\v]", RegexOptions.Compiled);

            
            var writePath = "D:\\SplitedBySentences.txt";
            WriteToFile(sentences, writePath);
            return sentences;
        }

        public void SplitByWords()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[!.?(),\"\\-;:\\s]", " ");
            var words = newText.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedByWords.txt";

            WriteToFile(words, writePath);
        }

        public void SplitByPunctuationMarks()
        {
            var text = FileReader();
            var expressions = text.Split(new char[] {'!', '.', '?', ',', '(', ')', '\"', '-', ';', ':'},
                StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedByPunctuationMark.txt";

            WriteToFile(expressions, writePath);
        }

        public void SortByAlphabet()
        {
            var reader = new StreamReader("D:\\SplitedByWords.txt");
            var text = reader.ReadToEnd();

            var words = text.Split(new char[] {' '}).ToList();
            var sortedWords = words
                .Select(g => new
                {
                    Word = g,
                    Count = g.Distinct().Count()
                })
                .ToList();

            var writer = new StreamWriter("D:\\SortedByAlphabet.txt", false);
            foreach (var expression in sortedWords)
            {
                writer.WriteLine($"{expression.Word}- {expression.Count}");
            }
        }

        public void WriteToFile(string[] expressions, string writePath)
        {
            var writer = new StreamWriter(writePath, false);
            foreach (var expression in expressions)
            {
                writer.WriteLine(expression.Trim());
            }
        }

        public void DisplayLongestSentenceBySymbols()
        {
            var reader = new StreamReader("D:\\SplitedBySentences.txt");
            var text = reader.ReadToEnd();
            
        }
    }
}
