using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace MyExpr.VisualStudio
{
    #region Format definition
    /// <summary>
    /// Defines an editor format for the MyExpr.VisualStudio type that has a purple background
    /// and is underlined.
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "MyExprLiteral")]
    [Name("MyExprLiteral")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class MyExprLiteral : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "MyExpr.VisualStudio" classification type
        /// </summary>
        public MyExprLiteral()
        {
            this.DisplayName = "MyExpr Literal"; //human readable version of the name
            this.BackgroundColor = Colors.Red;
            //this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "MyExprOperator")]
    [Name("MyExprOperator")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class MyExprOperator : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "MyExpr.VisualStudio" classification type
        /// </summary>
        public MyExprOperator()
        {
            this.DisplayName = "MyExpr Operator"; //human readable version of the name
            this.BackgroundColor = Colors.Gray;
            //this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "MyExprIdentifier")]
    [Name("MyExprIdentifier")]
    [UserVisible(true)] //this should be visible to the end user
    [Order(Before = Priority.Default)] //set the priority to be after the default classifiers
    internal sealed class MyExprIdentifier : ClassificationFormatDefinition
    {
        /// <summary>
        /// Defines the visual format for the "MyExpr.VisualStudio" classification type
        /// </summary>
        public MyExprIdentifier()
        {
            this.DisplayName = "MyExpr Identifier"; //human readable version of the name
            this.BackgroundColor = Colors.White;
            //this.TextDecorations = System.Windows.TextDecorations.Underline;
        }
    }
    #endregion //Format definition
}
