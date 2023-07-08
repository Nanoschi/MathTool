using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathTool;

namespace UnitTests
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void Test1()
        {
            string src = "1 + 1";
            Expr e = Expr.GenTree(src);
            Assert.AreEqual(2, e.Eval());
        }

        [TestMethod]
        public void Test2()
        {
            string src = "(1 + 1) * 2";
            Expr e = Expr.GenTree(src);
            Assert.AreEqual(2, e.Eval());
        }
    }
}