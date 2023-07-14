using System.Collections;
using System.Diagnostics;
using System.Text;
using UnitTests;
using System.Collections.Generic;

namespace MathTool
{
    class MathToolUI
    {
        private Dictionary<string, Expr> Variables = new Dictionary<string, Expr>();

        public MathToolUI() { }

        public void Run()
        {
            while (true)
            {
                string? input = Console.ReadLine();
                

            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string expr = "sign(-5) * sqrt(2) + sin(cos(12) + 56)";
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