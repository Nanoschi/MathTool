using System.Diagnostics;
using System.Text;
using UnitTests;

namespace MathTool
{
    

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(NumberExpr.StringToDouble("."));
            string expr = "((2 + 5.4)  * (3 - 5)) / 3.4 + 40 * 130 / 235";
            Test.DisplayExpression(expr);
            
            Test.TimeCreateTree(expr);
            Test.TimeEvalExpr(expr);
            
            Test.RunFullParserTest();

        }
    }
}