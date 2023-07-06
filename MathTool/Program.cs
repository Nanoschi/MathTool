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
            string src = "abc * 335 + 666";
            
            Console.WriteLine(Expr.GenTree(src));

        }
    }
}