using GrandTextAdventure.Core.Parsing;

namespace GrandTextAdventure.Core.Parser.Syntax
{
    public class LiteralNode : SyntaxNode
    {
        public LiteralNode(object value)
        {
            Value = value;
        }

        public object Value { get; set; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
