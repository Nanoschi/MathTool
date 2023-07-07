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
            // Parser test
            string src = "2 + 43 * 2";
            Console.Write("Tokens: ");
            List<Token> tokens = Token.Tokenize(src);
            Expr.TrimParens(tokens);
            foreach (Token t in tokens)
            {
                Console.Write(t);
            }

            Console.WriteLine();
            Expr e = Expr.GenTree(src);
            Console.WriteLine("Tree: " + e);
            Console.WriteLine("Value: " + e.Eval());

            //// Performance test
            //Stopwatch stopwatch = new Stopwatch();
            //Console.WriteLine("Start!");
            //stopwatch.Start();

            //for (int i = 0; i < 1000; i++)
            //{
            //    e.Eval();
            //}
            //stopwatch.Stop();
            //Console.WriteLine("Done! Elapsed: " + stopwatch.ElapsedMilliseconds + "ms");

        }
    }
}