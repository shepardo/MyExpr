using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExpr.Parser;
using Antlr.Runtime;
using Antlr.Runtime.Tree;

namespace MyExpr.VisualStudio
{
    static class ParserUtil
    {
        public static CommonTokenStream GetTokenStream(string code)
        {
            //MyExpr.Parser.
            MemoryStream ms = new MemoryStream(ASCIIEncoding.ASCII.GetBytes(code));
            CaseInsensitiveInputStream input = new CaseInsensitiveInputStream(ms);
            //MyExprParser
            MyExprLexer lexer = new MyExprLexer(input);
            CommonTokenStream stream = new CommonTokenStream(lexer);
            return stream;
        }

        public static MyExprParser.program_return Parse(string code, out StringBuilder errs)
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
            
            return r;
        }
    }
}
