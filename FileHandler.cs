using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Homework.ITAcademy6
{
    public class FileHandler
    {
        private const string PATH = "D:\\sample.txt";

        public string FileReader()
        {
            var reader = new StreamReader(PATH);
            return reader.ReadToEnd();
        }

        public string[] SplitIntoSentences()
        {
            var text = FileReader();
            var separators = new char[] { '!', '.', '?' };
            var sentences = text.Split(separators);
            return sentences;
        }

        public string[] SplitIntoWords()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[-!.?(),\"\\-;:]", "");
            var words = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }

        public string[] SplitByPunctuationMarks()
        {
            var text = FileReader();
            var separators = new char[] { '!', '.', '?', '(', ')', '"', '-', ';',':'};
            var stringsSplitedByPunctuationMarks = text.Split(separators);
            return null;
        }
    }
}