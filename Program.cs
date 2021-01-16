using System;
using System.IO;
using System.Threading.Tasks;

namespace Homework.ITAcademy6
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            var handler = new FileHandler();

            handler.WriteSplitedFiles();

            handler.SortByAlphabet();

            handler.WriteAdditionalDataFile();
        }

    }
}
