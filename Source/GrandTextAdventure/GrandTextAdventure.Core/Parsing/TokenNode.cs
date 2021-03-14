using GrandTextAdventure.Core.Parser;
using GrandTextAdventure.Core.Parsing.Text;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsing
{
    public class TokenNode : SyntaxNode
    {
        public TokenNode(Token token)
        {
            Token = token;
        }

        public Token Token { get; set; }
        public override TextSpan Span => Token.Span;

        public override void Accept(IScriptVisitor visitor)
        {
        }
    }
}
