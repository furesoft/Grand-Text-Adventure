using System;
using GrandTextAdventure.Core.Parsing;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsers.EntityParser.Syntax
{
    public class PropertyDefinitionNode : SyntaxNode
    {
        public PropertyDefinitionNode(Token keywordToken, Token nameToken, Token equalsToken, Token valueToken)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
            EqualsToken = equalsToken;
            Value = valueToken;
        }

        public Token EqualsToken { get; }
        public Token KeywordToken { get; }
        public Token NameToken { get; }
        public Token Value { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
