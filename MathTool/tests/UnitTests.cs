using MathTool;
using System.Diagnostics;

namespace UnitTests
{
    public static class Test
    {
        private static (string, double)[] _parser_test_cases =
        {
            ("2", 2.0),
            ("1 + 2", 3.0),
            ("1 + 2 / 4 - 6 * 7", -40.5),
            ("(1 + 2)", 3.0),
            ("(1) + (2)", 3.0),
            ("((1) + (2))", 3.0),
            ("(1) + 2", 3.0),
            ("3.2 + 1.7", 4.9),
            ("(((3 + 6 * 2) - (4 * 12 / 3)) - 5) + 7", 1.0),
            ("-1 + 3", 2),
            ("-1 + (3)", 2),
            ("-(1 + 2)", -3),
            ("4 + -(1 + 2)", 1),
        };

        private const string _performance_expr = "((2 + 3) * (5 - 13.5))";
        public static bool RunParserTest(string expr, double expected)
        {
            Console.WriteLine();
            Expr e = Expr.CreateTree(expr);
            double value = e.Eval();
            string passed = (value == expected) ? "Passed" : "FAILED!";
            Console.WriteLine($"{expr}: {e} = {value} | {passed}");
            return value == expected;
        }

        public static bool RunFullParserTest()
        {
            Console.WriteLine();
            bool success = true;
            foreach ((string, double) test in _parser_test_cases )
            {
                success = success && RunParserTest(test.Item1, test.Item2);
            }
            return success;
        }

        public static long TimeFullExpression(string expr = _performance_expr, int iterations = 1_000_000)
        {
            Console.WriteLine();
            
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"Full Perfomance Test Start! ({iterations})");

            sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                Expr e = Expr.CreateTree(expr);
                e.Eval();
            }
            sw.Stop();
            long time_ms = sw.ElapsedMilliseconds;
            Console.WriteLine($"Done! Total time: {time_ms}ms | Timer per 1,000,000: {((double)time_ms / (double)iterations) * 1_000_000}ms\n");
            return time_ms;
        }
        public static long TimeEvalExpr(string expr = _performance_expr, int iterations = 1_000_000)
        {
            Console.WriteLine();
            Expr e = Expr.CreateTree(expr);
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"Evaluation Start! ({iterations})");

            sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                e.Eval();
            }
            sw.Stop();
            long time_ms = sw.ElapsedMilliseconds;
            Console.WriteLine($"Done! Total time: {time_ms}ms | Timer per 1,000,000: {((double)time_ms / (double)iterations) * 1_000_000}ms\n");
            return time_ms;
        }
        public static long TimeCreateTree(string expr = _performance_expr, int iterations = 1_000_000)
        {
            Console.WriteLine();
            List<Token> tokens = Token.Tokenize(expr);
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"Tree Creation Start! ({iterations})");

            sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                Expr.CreateTree(tokens);
            }
            sw.Stop();
            long time_ms = sw.ElapsedMilliseconds;
            Console.WriteLine($"Done! Total time: {time_ms}ms | Timer per 1,000,000: {((double)time_ms / (double)iterations) * 1_000_000}ms\n");
            return time_ms;
        }
        public static long TimeTokenize(string expr = _performance_expr, int iterations = 1_000_000)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine($"Tokenizing Start! ({iterations})");
            sw.Start();
            for (int i = 0;  i < iterations; i++)
            {
                Token.Tokenize(expr);
            }
            sw.Stop();
            long time_ms = sw.ElapsedMilliseconds;
            Console.WriteLine($"Done! Total time: {time_ms}ms | Timer per 1,000,000: {((double)time_ms / (double)iterations) * 1_000_000}ms");
            return time_ms;
        }

        public static void DisplayExpression(string expr)
        {
            Console.WriteLine();
            Console.WriteLine("Source: " + expr);
            List<Token> tokens = Token.Tokenize(expr);
            Console.Write("Tokens: ");

            foreach (Token t in tokens)
            {
                Console.Write(t);
            }
            Console.WriteLine();
            Expr tree = Expr.CreateTree(tokens);
            Console.WriteLine("Tree: " + tree);
            Console.WriteLine("Value: " + tree.Eval());

        }
    }
}