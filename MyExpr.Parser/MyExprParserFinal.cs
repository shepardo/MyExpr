using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime;

namespace MyExpr.Parser
{
    public partial class MyExprParser : Antlr.Runtime.Parser
    {

    }
    public class MyExprParserFinal : MyExprParser
    {
        public MyExprParserFinal(ITokenStream input)
            : base(input)
        {

        }

        public override string GetErrorMessage(
        RecognitionException e,
        string[] tokenNames)
    {
      if (e is NoViableAltException)
      {
         
        NoViableAltException nvae = (e as NoViableAltException);
        if (nvae.GrammarDecisionDescription.Contains("operand expected")) 
        {
          /* Returns previous implementation format plus expected rule */
          return string.Format("no viable alternative at input {0}. Expected {1}.",
            this.GetTokenErrorDisplay(e.Token), (e as NoViableAltException).GrammarDecisionDescription);
        }
      }
      return base.GetErrorMessage(e, tokenNames);
    }
    }
}
