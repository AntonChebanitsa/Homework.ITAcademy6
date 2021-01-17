using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public string[] SplitBySentences(string text)
        {
            var sentences = Regex.Split(text, "[!.?;\"\\t\\r\\v\\n]", RegexOptions.Compiled);

            return sentences.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); ;
        }

        public string[] SplitByWords(string text)
        {
            var newText = Regex.Replace(text, "[^a-zA-Z]", " ");

            return newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); ;
        }

        public string[] SplitByPunctuationMarks(string text)
        {
            var newText = Regex.Replace(text, @"[^!.?,()\-:\;]", " ")
                .Split(" ", StringSplitOptions.None);

            return newText.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); ;
        }


        public async Task SortByAlphabet()
        {
            var words = await File.ReadAllTextAsync(_pathToReadingWritingFolder + "SplitedByWords.txt");
            var splited = Regex.Split(words, @"\s", RegexOptions.Compiled);

            var sortedWords = splited
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
            var text = await File.ReadAllTextAsync(_pathToReadingWritingFolder + "SplitedBySentences.txt");
            var sentences = text.Split("\n", StringSplitOptions.RemoveEmptyEntries);

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
            var text = await File.ReadAllTextAsync(_pathToReadingWritingFolder + "SplitedBySentences.txt");
            var sentences = text.Split("\n\r", StringSplitOptions.RemoveEmptyEntries);

            var shortestSentence = "";
            var elements = 5;
            foreach (var sentence in sentences)
            {
                foreach (var character in sentence.ToCharArray())
                {
                    var counter = 0;

                    if (character == ' ')
                        counter++;

                    if (counter > elements)
                    {
                        elements = counter;
                        shortestSentence = sentence;
                    }
                }
            }

            return $"The shortest sentence in terms of the number of words is: {shortestSentence}.\n" +
                   $"Number of words is {elements}"; ;
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
                }
            }
        }

        public async Task WriteSplitedFiles(string text)
        {
            await WriteToFile(SplitBySentences(text), _pathToReadingWritingFolder + "SplitedBySentences.txt");
            await WriteToFile(SplitByWords(text), _pathToReadingWritingFolder + "SplitedByWords.txt");
            await WriteToFile(SplitByPunctuationMarks(text), _pathToReadingWritingFolder + "SplitedByPunctuationMark.txt");
        }

        public async Task WriteAdditionalDataFile(string text)
        {
            var writePath = _pathToReadingWritingFolder + "AdditionalDataFile.txt";
            var str = $"{await DisplayLongestSentenceBySymbols()}~ {await DisplayShortestSentenceByWords()}~ {await MostCommonLetter()}";
            var arr = str.Split('~', StringSplitOptions.RemoveEmptyEntries);

            await WriteToFile(arr, writePath);
        }
    }
}
