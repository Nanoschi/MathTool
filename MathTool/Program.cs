using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MathTool
{

    class Program
    {
        static void Main(string[] args)
        {
            string src = "123 + carea";
            Console.WriteLine("Start!");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1_000_000; i++)
            {
                Token.Tokenize(src);
            }
            sw.Stop();
            Console.WriteLine("Done! Elapsed Time: " + sw.ElapsedMilliseconds + "ms");

        }
    }
}