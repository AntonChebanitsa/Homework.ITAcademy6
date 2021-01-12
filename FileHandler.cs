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
            var writePath = "D:\\SplitedBySentences.txt";

            var text = FileReader();
            var separators = new string[] { "! ", ". ", "? ", "\t", "\n", "!", "?", ".", }; //TODO Add RegEx Here
            var sentences = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var writer = new StreamWriter(writePath, false);
            foreach (var sentence in sentences)
            {
                if (sentence[0] == ' ')
                {
                    sentence[0].ToString().TrimStart(' ');
                }
                writer.WriteLine(sentence);
            }
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
            var separators = new char[] { '!', '.', '?', '(', ')', '"', '-', ';', ':' };
            var textSplitedByPunctuationMarks = text.Split(separators);
            return textSplitedByPunctuationMarks;
        }

        public void WriteToFile()
        {

        }
    }
}