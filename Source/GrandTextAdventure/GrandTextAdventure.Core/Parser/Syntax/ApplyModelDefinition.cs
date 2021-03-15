using GrandTextAdventure.Core.Parsing;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parser.Syntax
{
    public class ApplyModelDefinition : SyntaxNode
    {
        public ApplyModelDefinition(Token keywordToken, Token nameToken)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
        }

        public Token KeywordToken { get; }
        public Token NameToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
