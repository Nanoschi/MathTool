using System.Collections;
using System.Diagnostics;
using System.Text;
using UnitTests;
using System.Collections.Generic;

namespace MathTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string expr = "sin(2 * 6 - 2) / sqrt(1123 - 234) + ceil(125.23)";
            Test.DisplayExpression(expr);
            
            Test.TimeTokenize(expr);
            Test.TimeCreateTree(expr);
            Test.TimeEvalExpr(expr);
            
            //Test.RunFullParserTest();

            //MathToolUI ui = new MathToolUI();
            //ui.Run();
            

        }
    }
}