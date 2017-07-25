using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Antlr.Runtime;
using Antlr.Runtime.Tree;

namespace MyExpr.Parser.Tests
{
    [TestClass]
    public class ValidExpressions
    {
        [TestMethod]
        public void ValidExpr1()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a + b;", out sb, false);
            return;
        }

        [TestMethod]
        public void ValidExpr2()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a + b - 4;", out sb, false);
        }

        [TestMethod]
        public void ValidExpr3()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("b := a + b - 4;", out sb, false);
        }

        [TestMethod]
        public void ValidExpr4()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a - c;", out sb, false);
        }

        [TestMethod]
        public void ValidExpr5()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("4 + 5 - 8;", out sb, false);
        }

        [TestMethod]
        public void ValidExpr6()
        {
            StringBuilder sb;
            MyExprParser.program_return r = Util.Parse("a := 4 + 5 - 8; b:= a + 1; 5 + 4;", out sb, false);
            CommonTree ct = (CommonTree)r.Tree;
            Assert.AreEqual(ct.Children.Count, 4);

            ITree child = ct.GetChild(0);
            Assert.AreEqual(child.Text, ":=");
            Assert.AreEqual(child.ChildCount, 2);
            ITree child2 = child.GetChild(0);
            Assert.AreEqual(child2.Type, MyExprLexer.IDENTIFIER);
            Assert.AreEqual(child2.Text, "a");
            child2 = child.GetChild(1);
            Assert.AreEqual(child2.Type, MyExprParser.TOK_EXPR);
            ITree child3 = child2.GetChild(0);
            Assert.AreEqual(child3.Type, MyExprLexer.INT_NUMBER);
            Assert.AreEqual(child3.Text, "4");
            
            child3 = child2.GetChild(1);
            Assert.AreEqual(child3.Type, MyExprLexer.PLUS);
            Assert.AreEqual(child3.ChildCount, 2);
            ITree child4 = child3.GetChild(0);
            Assert.AreEqual(child4.Type, MyExprLexer.INT_NUMBER);
            Assert.AreEqual(child4.Text, "5");
            child4 = child3.GetChild(1);
            Assert.AreEqual(child4.Type, MyExprLexer.MINUS);
            Assert.AreEqual(child4.ChildCount, 1);
            child4 = child4.GetChild(0);
            Assert.AreEqual(child4.Type, MyExprLexer.INT_NUMBER);
            Assert.AreEqual(child4.Text, "8");

            child = ct.GetChild(1);
            Assert.AreEqual(child.Text, ":=");
            Assert.AreEqual(child.ChildCount, 2);
            child2 = child.GetChild(0);
            Assert.AreEqual(child2.Type, MyExprLexer.IDENTIFIER);
            Assert.AreEqual(child2.Text, "b");
            child2 = child.GetChild(1);
            Assert.AreEqual(child2.Type, MyExprParser.TOK_EXPR);
            Assert.AreEqual(child2.ChildCount, 2);
            child3 = child2.GetChild(0);
            Assert.AreEqual(child3.Type, MyExprLexer.IDENTIFIER);
            Assert.AreEqual(child3.Text, "a");
            child3 = child2.GetChild(1);
            Assert.AreEqual(child3.Type, MyExprLexer.PLUS);
            Assert.AreEqual(child3.ChildCount, 1);
            child4 = child3.GetChild(0);
            Assert.AreEqual(child4.Type, MyExprLexer.INT_NUMBER);
            Assert.AreEqual(child4.Text, "1");

            child = ct.GetChild(2);
            Assert.AreEqual(child.Type, MyExprParser.TOK_EXPR);
            Assert.AreEqual(child.ChildCount, 2);
            child2 = child.GetChild(0);
            Assert.AreEqual(child2.Type, MyExprLexer.INT_NUMBER);
            Assert.AreEqual(child2.Text, "5");
            child2 = child.GetChild(1);
            Assert.AreEqual(child2.Type, MyExprLexer.PLUS);
            Assert.AreEqual(child2.ChildCount, 1);
            child3 = child2.GetChild(0);
            Assert.AreEqual(child3.Type, MyExprLexer.INT_NUMBER);
            Assert.AreEqual(child3.Text, "4");

            child = ct.GetChild(3);
            Assert.AreEqual(child.Type, MyExprLexer.EOF);


            return;
        }
    }
}
