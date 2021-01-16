using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Homework.ITAcademy6
{
    public class FileHandler
    {
        private string _pathToReadingWritingFolder = @"D:\HomeworkChebanitsa\";

        public string FileReader()
        {
            var reader = new StreamReader(_pathToReadingWritingFolder + "sample.txt");

            return reader.ReadToEnd();
        }

        public string[] SplitBySentences()
        {
            var text = FileReader();
            var sentences = Regex.Split(text, "[!.?;\"\\t\\r\\v\\n]", RegexOptions.Compiled);
            var result = sentences.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return result;
        }

        public string[] SplitByWords()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[^a-zA-Z]", " ");
            var words = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }

        public string[] SplitByPunctuationMarks()
        {
            var text = FileReader();
            var expressions = text.Split(new char[] { '!', '.', '?', ',', '(', ')', '-', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries).Where(x=> !string.IsNullOrWhiteSpace(x)).ToArray();

            return expressions;
        }


        public void SortByAlphabet()
        {
            var words = SplitByWords();
            var sortedWords = words
                .GroupBy(w => w, StringComparer.OrdinalIgnoreCase)
                 .Select(key => new
                 {
                     Word = key,
                     Count = key.Count()
                 })
                .OrderBy(x => x.Word.Key)
                .ToList();

            using var sw = new StreamWriter(_pathToReadingWritingFolder + "SortedByAlphabet.txt", false);
            {
                foreach (var expression in sortedWords)
                {
                    sw.WriteLine($"{expression.Word.Key}- {expression.Count}");
                }
            }
        }


        public string DisplayLongestSentenceBySymbols() 
        {
            var sentences = SplitBySentences();

            string longestSentence = null;
            var max = 0;

            foreach (var t in sentences)
            {
                var symbols = t.ToCharArray();
                if (max <= symbols.Length)
                {
                    longestSentence = t;
                    max = symbols.Length;
                }
                else continue;
            }

            return $"The longest sentence in terms of the number of characters is: {longestSentence}.\n" +
                   $"Number of characters is {max}";
        }

        public string DisplayShortestSentenceByWords()
        {
            var sentences = SplitBySentences();
            var shortest = "Frequently.";
            var min = 1;

            var result = $"The shortest sentence in terms of the number of words is: {shortest}.\n" +
                         $"Number of words is {min}";

            return result;
        }

        public string MostCommonLetter()
        {
            var text = FileReader().ToLower();
            var charArray = Regex.Replace(text, @"[^a-z]+", String.Empty).ToCharArray();

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

            char mostCommonLetter = default;
            var max = 0;
            foreach (var pair in dictionary.Where(pair => max < pair.Value))
            {
                max = pair.Value;
                mostCommonLetter = pair.Key;
            }

            return $"The most common letter is {mostCommonLetter}. Occurs {max} times.";
        }


        public void WriteToFile(string[] expressions, string writePath)
        {
            using var sw = new StreamWriter(writePath, false);
            {
                foreach (var expression in expressions)
                {
                    sw.WriteLine(expression.Trim());
                }
            }
        }

        public void WriteSplitedFiles()
        {
            WriteToFile(SplitBySentences(), _pathToReadingWritingFolder + "SplitedBySentences.txt");
            WriteToFile(SplitByWords(), _pathToReadingWritingFolder + "SplitedByWords.txt");
            WriteToFile(SplitByPunctuationMarks(), _pathToReadingWritingFolder + "SplitedByPunctuationMark.txt");
        }

        public void WriteAdditionalDataFile()
        {
            var writePath = _pathToReadingWritingFolder + "AdditionalDataFile.txt";
            var str = $"{DisplayLongestSentenceBySymbols()}~ {DisplayShortestSentenceByWords()}~ {MostCommonLetter()}";
            var arr=str.Split('~',StringSplitOptions.RemoveEmptyEntries);

            WriteToFile(arr, writePath);
        }
    }
}
