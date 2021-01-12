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
            var sentences = Regex.Split(text, "[!.?;\\t]",RegexOptions.None);


            var writePath = "D:\\SplitedBySentences.txt";
            var writer = new StreamWriter(writePath, false);
            foreach (var sentence in sentences)
            {
                writer.WriteLine(sentence.Trim());
            }
        }

        public void SplitByWords()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[!.?(),\"\\-;:\\t\\n]", " ");
            var words = newText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedByWords.txt";
            var writer = new StreamWriter(writePath, false);
            foreach (var word in words)
            {
                writer.WriteLine(word);
            }
        }

        public void SplitByPunctuationMarks()
        {
            var text = FileReader();
            var newText = Regex.Replace(text, "[!.?(),\"\\-;:\\n]", "~");
            var words = newText.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);

            var writePath = "D:\\SplitedByPunctuationMark.txt";
            var writer = new StreamWriter(writePath, false);
            foreach (var word in words)
            {
                writer.WriteLine(word.Trim());
            }
        }
    }
}