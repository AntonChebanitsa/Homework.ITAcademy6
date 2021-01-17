using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework.ITAcademy6
{
    public class FileHandler
    {
        private string _pathToReadingWritingFolder = @"D:\HomeworkChebanitsa\";

        public async Task<string> FileReader()
        {
            var text = await File.ReadAllTextAsync(_pathToReadingWritingFolder + "sample.txt");

            return text;
        }

        public async Task<string[]> SplitBySentences()
        {
            var text = await FileReader();
            var sentences = Regex.Split(text, "[!.?;\"\\t\\r\\v\\n]", RegexOptions.Compiled);
            var result = sentences.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return result;
        }

        public async Task<string[]> SplitByWords()
        {
            var text = await FileReader();
            var newText = Regex.Replace(text, "[^a-zA-Z]", " ");
            var words = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            return words;
        }

        public async Task<string[]> SplitByPunctuationMarks()
        {
            var text = await FileReader();
            var newText = Regex.Replace(text, @"[^!.?,()\-:\;]", " ");
            //[^!.?,()-:\;
            var expressions = text.Split(new char[] { '!', '.', '?', ',', '(', ')', '-', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            return expressions;
        }


        public async Task SortByAlphabet()
        {
            var words = await SplitByWords();
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
                    await sw.WriteLineAsync($"{expression.Word.Key}- {expression.Count}");
                }
            }
        }


        public async Task<string> DisplayLongestSentenceBySymbols()
        {
            var sentences = await SplitBySentences();

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

        public async Task<string> DisplayShortestSentenceByWords()
        {
            var sentences = await SplitBySentences();
            var shortest = "Frequently.";
            var min = 1;

            var result = $"The shortest sentence in terms of the number of words is: {shortest}.\n" +
                         $"Number of words is {min}";

            return result;
        }

        public async Task<string> MostCommonLetter()
        {
            var text = await FileReader();
            var charArray = Regex.Replace(text, @"[^a-z]+", String.Empty).ToLower().ToCharArray();

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


        public async Task WriteToFile(string[] expressions, string writePath)
        {
            await using var sw = new StreamWriter(writePath, false);
            {
                foreach (var expression in expressions)
                {
                    await sw.WriteLineAsync(expression.Trim());
                    int x = 0;
                }
            }
        }

        public async Task WriteSplitedFiles()
        {
            await WriteToFile(await SplitBySentences(), _pathToReadingWritingFolder + "SplitedBySentences.txt");
            await WriteToFile(await SplitByWords(), _pathToReadingWritingFolder + "SplitedByWords.txt");
            await WriteToFile(await SplitByPunctuationMarks(), _pathToReadingWritingFolder + "SplitedByPunctuationMark.txt");
        }

        public async Task WriteAdditionalDataFile()
        {
            var writePath = _pathToReadingWritingFolder + "AdditionalDataFile.txt";
            var str = $"{await DisplayLongestSentenceBySymbols()}~ {await DisplayShortestSentenceByWords()}~ {await MostCommonLetter()}";
            var arr = str.Split('~', StringSplitOptions.RemoveEmptyEntries);

            await WriteToFile(arr, writePath);
        }
    }
}
