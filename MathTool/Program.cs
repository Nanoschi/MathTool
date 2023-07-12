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
            string expr = "2 + -(3 + 7) * +(2 + 5)";
            Test.DisplayExpression(expr);
            
            Test.TimeCreateTree(expr, 10_000);
            Test.TimeEvalExpr(expr);
            
            Test.RunFullParserTest();

            MathToolUI ui = new MathToolUI();
            ui.Run();

        }
    }
}