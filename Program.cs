using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Homework.ITAcademy6
{
    class Program
    {
        static async Task Main(string[] args)
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
