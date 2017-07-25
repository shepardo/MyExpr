using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;

namespace MyExpr.VisualStudio
{
    [Export(typeof(ICompletionSourceProvider))]
    //[ContentType("myexpr")]
    [ContentType("text")]
    //[ContentType("plaintext")]
    [Name("token completion")]
    internal class MyExprCompletionSourceProvider : ICompletionSourceProvider
    {
        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new MyExprCompletionSource(this, textBuffer);
        }
    }
}
