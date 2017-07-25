using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyExpr.Parser.Tests
{
    [TestClass]
    public class CompletionTests
    {
        [TestMethod]
        public void MissingOperator1()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a + ;", out sb, true);
            Assert.AreEqual(true, sb.ToString().Contains("operand expected"));
        }

        [TestMethod]
        public void MissingOperator2()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("3 + ", out sb, true);
            Assert.AreEqual(true, sb.ToString().Contains("operand expected"));
        }

        [TestMethod]
        public void MissingOperator3()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("3 + 4 - ", out sb, true);
            Assert.AreEqual(true, sb.ToString().Contains("operand expected"));
        }

        [TestMethod]
        public void MissingOperator4()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a := ", out sb, true);
        }

        [TestMethod]
        public void MissingOperator5()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a := 5", out sb, true);
        }

        [TestMethod]
        public void MissingOperator6()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a + b", out sb, true);
        }
    }
}
