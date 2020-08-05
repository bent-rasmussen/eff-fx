using Eff_Fx.FileSystemIO;
using System;
using System.Threading.Tasks;

namespace Eff_Fx
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Executing...");
            await FileSystemTest.Test(@"c:\");
            Console.WriteLine("Done.");
        }
    }
}
