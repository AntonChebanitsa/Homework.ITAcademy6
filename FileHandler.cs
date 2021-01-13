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

            return sentences;
        }

        public string[] SplitByWords()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[^a-zA-Z$]", " ");
            var words = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }

        public string[] SplitByPunctuationMarks()
        {
            var text = FileReader();
            var expressions = text.Split(new char[] { '!', '.', '?', ',', '(', ')', '\"', '-', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

            return expressions;
        }

        public void SortByAlphabet() //todo need to fix it
        {
            var words = SplitByWords();
            var sortedWords = words
                .GroupBy(w => w, StringComparer.OrdinalIgnoreCase)
                 .Select(key => new
                 {
                     Word = key,
                     Count = key.Count()
                 })
                .ToList()
                .OrderBy(x => x.Word.Key);
            

            var writer = new StreamWriter("D:\\SortedByAlphabet.txt", false);
            foreach (var expression in sortedWords)
            {
                writer.WriteLine($"{expression.Word.Key}- {expression.Count}");
            }
        }

        public void DisplayLongestSentenceBySymbols()
        {
            SplitBySentences();


        }

        public void DisplayShorterSentenceByWords()
        {
            SplitBySentences();


        }

        public void MostCommonLetter()
        {
            var text = FileReader().ToLower();
            var charArray = Regex.Replace(text, @"[^A-Z]+", String.Empty).ToCharArray();

            var dictionary = new Dictionary<char, int>();
            var counter = 1;
            foreach (var letter in charArray)
            {
                if (!dictionary.ContainsKey(letter))
                {
                    dictionary.Add(letter, counter);
                }
                else
                {
                    dictionary[letter] += 1;
                }

            }

            //После цикла проходите по всему словарю, и ищите максимальное значение.
            //Console.WriteLine(charArray.GroupBy(c => c).OrderByDescending(g => g.Count()).First().Key) ;


        }

        public void WriteToFile(string[] expressions, string writePath)
        {
            var writer = new StreamWriter(writePath, false);
            foreach (var expression in expressions)
            {
                writer.WriteLine(expression.Trim());
            }
        }

        public void WriteSplitedFiles()
        {
            WriteToFile(SplitBySentences(), "D:\\SplitedBySentences.txt");
            WriteToFile(SplitByWords(), "D:\\SplitedByWords.txt");
            WriteToFile(SplitByPunctuationMarks(), "D:\\SplitedByPunctuationMark.txt");
        }
    }
}
