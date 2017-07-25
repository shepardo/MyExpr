using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace MyExpr.VisualStudio
{
    internal static class MyExprClassifierClassificationDefinition
    {
        /// <summary>
        /// Defines the "Literal" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("MyExprLiteral")]
        internal static ClassificationTypeDefinition Literal = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("MyExprOperator")]
        internal static ClassificationTypeDefinition Operator = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("MyExprIdentifier")]
        internal static ClassificationTypeDefinition Identifier = null;
    }
}
