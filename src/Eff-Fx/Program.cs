using Eff_Fx.FileFx;
using System;
using System.Threading.Tasks;

namespace Eff_Fx
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Executing...");
            await FileTest.Test(@"c:\");
            Console.WriteLine("Done.");
        }
    }
}
