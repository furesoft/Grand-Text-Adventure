using GrandTextAdventure.Core.Parsing;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsers.EntityParser.Syntax
{
    public abstract class DeclarationNode : SyntaxNode
    {
        protected DeclarationNode(Token endToken)
        {
            EndToken = endToken;
        }

        public Token EndToken { get; set; }
    }
}
