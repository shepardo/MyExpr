using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;
using MyExpr.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyExpr.Parser.Tests
{
    public static class Util
    {
        public static MyExprParser.program_return Parse(string code, out StringBuilder errs, bool expectErrors)
        {
            errs = new StringBuilder();
            MemoryStream ms = new MemoryStream(ASCIIEncoding.ASCII.GetBytes(code));
            CaseInsensitiveInputStream input = new CaseInsensitiveInputStream(ms);
            MyExprLexer lexer = new MyExprLexer(input);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            MyExprParserFinal parser = new MyExprParserFinal(tokens);
            TextWriter tw = new StringWriter(errs);
            parser.TraceDestination = tw;
            MyExprParser.program_return r = parser.program();
            if (!expectErrors)
            {
                if (0 != parser.NumberOfSyntaxErrors)
                    Assert.AreEqual("", errs.ToString());
            }
            else
            {
                Assert.AreNotEqual(0, parser.NumberOfSyntaxErrors);
            }
            return r;
        }
    }
}
