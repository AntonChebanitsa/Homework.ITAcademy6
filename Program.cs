using System;
using System.IO;
using System.Threading.Tasks;

namespace Homework.ITAcademy6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Run();
        }

        private static async Task Run()
        {
            var handler = new FileHandler();

            var text=await handler.FileReader();

            await handler.WriteSplitedFiles(text);
            await handler.SortByAlphabet();
            await handler.WriteAdditionalDataFile(text);
        }
    }
}
