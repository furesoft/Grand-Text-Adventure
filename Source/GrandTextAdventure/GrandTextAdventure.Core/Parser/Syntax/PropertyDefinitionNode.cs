using GrandTextAdventure.Core.Parsing;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parser.Syntax
{
    public class PropertyDefinitionNode : SyntaxNode
    {
        public PropertyDefinitionNode(Token keywordToken, Token nameToken, Token equalsToken, LiteralNode valueLiteral)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
            EqualsToken = equalsToken;
            Value = valueLiteral;
        }

        public Token EqualsToken { get; }
        public Token KeywordToken { get; }
        public Token NameToken { get; }
        public LiteralNode Value { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
