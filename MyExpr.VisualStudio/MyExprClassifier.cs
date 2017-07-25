using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Antlr.Runtime;
using MyExpr.Parser;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell; 

namespace MyExpr.VisualStudio
{

    #region Provider definition
    /// <summary>
    /// This class causes a classifier to be added to the set of classifiers. Since 
    /// the content type is set to "text", this classifier applies to all text files
    /// </summary>
    [Export(typeof(IClassifierProvider))]
    //[ContentType("myexpr")]
    [ContentType("text")]
    internal class MyExprClassifierProvider : IClassifierProvider
    {
 

        /// <summary>
        /// Import the classification registry to be used for getting a reference
        /// to the custom classification type later.
        /// </summary>
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null; // Set via MEF

        [Import]
        internal SVsServiceProvider ServiceProvider = null; 

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            DTE dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            return buffer.Properties.GetOrCreateSingletonProperty<MyExprClassifier>(delegate { return new MyExprClassifier(ClassificationRegistry); });
        }
    }
    #endregion //provider def

    #region Classifier
    /// <summary>
    /// Classifier that classifies all text as an instance of the OrinaryClassifierType
    /// </summary>
    class MyExprClassifier : IClassifier
    {
        Dictionary<int, IClassificationType> _types = null;

        internal MyExprClassifier(IClassificationTypeRegistryService registry)
        {
            //_classificationType = registry.GetClassificationType("MyExprLiteral");
            BuildTypeList(registry);
        }

        private void BuildTypeList(IClassificationTypeRegistryService registry)
        {
            _types = new Dictionary<int, IClassificationType>();
            _types[MyExprLexer.ASSIGN] = registry.GetClassificationType("MyExprOperator");
            _types[MyExprLexer.INT_NUMBER] = registry.GetClassificationType("MyExprLiteral");
            _types[MyExprLexer.MINUS] = registry.GetClassificationType("MyExprOperator");
            _types[MyExprLexer.PLUS] = registry.GetClassificationType("MyExprOperator");
            _types[MyExprLexer.IDENTIFIER] = registry.GetClassificationType("MyExprIdentifier");

        }

        /// <summary>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </summary>
        /// <param name="trackingSpan">The span currently being classified</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            //create a list to hold the results
            List<ClassificationSpan> classifications = new List<ClassificationSpan>();

            foreach (var line in span.Snapshot.Lines)
            {
                ITextSnapshotLine containingLine = null;
                containingLine = line.Start.GetContainingLine();
                string input = containingLine.GetText();
                CommonTokenStream tokenStream = ParserUtil.GetTokenStream(input);
                tokenStream.Fill();
                List<IToken> tokens = tokenStream.GetTokens();
                for (int i = 0; i < tokens.Count; i++)
                {
                    IToken tok = tokens[i];
                    if (tok.Type == MyExprLexer.EOF) continue;

                    int startIndex = containingLine.Start + tok.StartIndex;
                    SnapshotSpan snapshotSpan = new SnapshotSpan(span.Snapshot, new Span(startIndex, tok.Text.Length));
                    if (snapshotSpan.IntersectsWith(span))
                    {
                        IClassificationType type = GetTokenType(tok);
                        if(type != null)
                            classifications.Add(new ClassificationSpan(snapshotSpan, type));
                    }
                }
            }

            //classifications.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)),
            //                                               _classificationType));
            return classifications;
        }

        private IClassificationType GetTokenType(IToken token)
        {
            IClassificationType result = null;
            if (_types.TryGetValue(token.Type, out result))
                return result;
            else
                return null;
        }

#pragma warning disable 67
        // This event gets raised if a non-text change would affect the classification in some way,
        // for example typing /* would cause the classification to change in C# without directly
        // affecting the span.
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 67
    }
    #endregion //Classifier
}
