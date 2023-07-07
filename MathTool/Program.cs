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
            string src = "5 - 1 + 6";
            Expr e = Expr.GenTree(src);
            Console.WriteLine(e);
            Console.WriteLine(e.Eval());

        }
    }
}