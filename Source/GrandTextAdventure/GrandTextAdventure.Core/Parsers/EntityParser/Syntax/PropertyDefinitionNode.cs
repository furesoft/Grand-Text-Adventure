using System;
using GrandTextAdventure.Core.Parsing;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsers.EntityParser.Syntax
{
    public class PropertyDefinitionNode : SyntaxNode
    {
        public PropertyDefinitionNode(Token keywordToken, Token nameToken, SyntaxNode value)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
            Value = value;
        }

        public Token KeywordToken { get; }
        public Token NameToken { get; }
        public SyntaxNode Value { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
