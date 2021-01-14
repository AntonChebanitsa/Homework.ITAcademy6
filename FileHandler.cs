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
        private string _pathToReadingWritingFolder = "D:\\";

        public string FileReader()
        {
            var reader = new StreamReader(_pathToReadingWritingFolder + "sample.txt");

            return reader.ReadToEnd();
        }

        public string[] SplitBySentences()// todo wrong!! need correct regex or another way
        {
            var text = FileReader();
            var sentences = Regex.Split(text, @"(?<!\w\.\w.)(?<![A-Z][a-z]\.)(?<=\.|\?)\s", RegexOptions.Compiled);
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
            var expressions = text.Split(new char[] { '!', '.', '?', ',', '(', ')', '\"', '-', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

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

            var writer = new StreamWriter("D:\\SortedByAlphabet.txt", false);
            foreach (var expression in sortedWords)
            {
                writer.WriteLine($"{expression.Word.Key}- {expression.Count}");
            }
        }


        public string DisplayLongestSentenceBySymbols() //check after changing the split pattern
        {
            var sentences = SplitBySentences();

            string longestSentence = null;
            var max = 0;
            foreach (var t in sentences)
            {
                var symbols = t.ToCharArray();
                if (max >= symbols.Length) continue;
                longestSentence = t;
                max = symbols.Length;
            }

            var result = $"The longest sentence in terms of the number of characters is: {longestSentence}.\n" +
                         $"Number of characters is {max}";

            Console.WriteLine(result);

            return result;
        }

        public void DisplayShortestSentenceByWords()//todo wrong job due to split(possible)
        {
            var sentences = SplitBySentences();

            string shortestSentence = null;
            var min = 1000;
            var counter = 0;
            foreach (var sentence in sentences)
            {
                int NumberOfWords = sentence.Split(' ').Length;
                Console.WriteLine($"{NumberOfWords + 1}, {sentence}");
                counter++;
                if (counter==100)break;
            }

            Console.WriteLine($"{min} {shortestSentence}");

            var result = $"The shortest sentence in terms of the number of words is: {shortestSentence}.\n" +
                         $"Number of words is {min}";

            //Console.WriteLine(result);

            //return result;

            // o самое короткое предложение по количеству слов


        }// todo

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

            var result = $"The most common letter is {mostCommonLetter}. Occurs {max} times."; // todo remove this where added writing to file
            Console.WriteLine(result);
            return result;
        }//todo


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
            WriteToFile(SplitBySentences(), _pathToReadingWritingFolder + "SplitedBySentences.txt");
            WriteToFile(SplitByWords(), _pathToReadingWritingFolder + "SplitedByWords.txt");
            WriteToFile(SplitByPunctuationMarks(), _pathToReadingWritingFolder + "SplitedByPunctuationMark.txt");
        }

        public void WriteAdditionalDataFile()
        {
            var writePath = _pathToReadingWritingFolder + "AdditionalDataFile.txt";
            var writer = new StreamWriter(writePath, false);

            //writer.WriteLine(DisplayLongestSentenceBySymbols());
            //writer.WriteLine(DisplayShorterSentenceByWords());
            writer.WriteLine(MostCommonLetter());
        }//todo need to be implemented
    }
}
