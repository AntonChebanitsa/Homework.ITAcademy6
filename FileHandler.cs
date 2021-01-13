using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

        public void SplitBySentences()
        {
            var text = FileReader();
            var sentences = Regex.Split(text, "[!.?;\\t\\r\\v]", RegexOptions.Compiled);


            var writePath = "D:\\SplitedBySentences.txt";
            WriteToFile(sentences, writePath);
        }

        public void SplitByWords()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[!.?(),\"\\-;:\\s]", " ");
            var words = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedByWords.txt";

            WriteToFile(words, writePath);
        }

        public void SplitByPunctuationMarks()
        {
            var text = FileReader();
            var expressions = text.Split(new char[] { '!', '.', '?', ',', '(', ')', '\"', '-', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedByPunctuationMark.txt";

            WriteToFile(expressions,writePath);
        }

        public void SortByAlphabet()
        {
            var path = "D:\\SplitedByWords.txt";
            var writePath = "D:\\SortedByAlphabet.txt";
            var reader = new StreamReader(path);

            var text=reader.ReadToEnd();
            var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var sorted= Array.Sort(words, (p, q) => p[0].CompareTo(q[0]));
            WriteToFile(words, writePath);

        }

        public void WriteToFile(string[] expressions, string writePath)
        {
            var writer = new StreamWriter(writePath, false);
            foreach (var expression in expressions)
            {
                writer.WriteLine(expression.Trim());
            }
        }
    }
}