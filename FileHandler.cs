using System;
using System.IO;
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
            var newText = Regex.Split(text, "[!.?;:\\n\\t]",RegexOptions.None);
            //var separators = new char[] { '!', '.', '?', '\t', '\n', ';' };
            //var sentences = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedBySentences.txt";
            var writer = new StreamWriter(writePath, false);
            foreach (var sentence in newText)
            {
                writer.WriteLine(sentence.Trim());
            }
        }

        public void SplitByWords()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[!.?(),\"\\-;:\t \n]", " ");
            var words = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedByWords.txt";
            var writer = new StreamWriter(writePath, false);
            foreach (var word in words)
            {
                writer.WriteLine(word);
            }
        }

        public string[] SplitByPunctuationMarks()
        {
            var text = FileReader();
            var separators = new char[] { '!', '.', '?', '(', ')', '"', '-', ';', ':' };
            var textSplitedByPunctuationMarks = text.Split(separators);
            return textSplitedByPunctuationMarks;
        }

        public void WriteToFile()
        {

        }
    }
}