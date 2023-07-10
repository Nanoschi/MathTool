using MathTool;

namespace UnitTests
{
    public static class Test
    {
        private static (string, double)[] _parser_test_cases =
        {
            ("4", 4.0),
            ("2 + 2", 4.0),
            ("2 + 2 / 2 - 2 * 4", -5.0),
            ("(2 + 2)", 4.0),
            ("((2 + 2))", 4.0),
            ("3.2 + 1.7", 4.9),
            ("(((3 + 6 * 2) - (4 * 12 / 3)) - 5) + 7", 1.0),
        };
        public static bool RunParserTest(string expr, double expected)
        {
            Expr e = Expr.CreateTree(expr);
            double value = e.Eval();
            string passed = (value == expected) ? "Passed" : "FAILED!";
            Console.WriteLine($"{expr}: {e} = {value} | {passed}");
            return value == expected;
        }

        public static bool RunFullParserTest()
        {
            bool success = true;
            foreach ((string, double) test in _parser_test_cases )
            {
                success = success && RunParserTest(test.Item1, test.Item2);
            }
            return success;
        }
    }
}