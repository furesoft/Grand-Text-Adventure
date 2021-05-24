using GrandTextAdventure.Core.Parsing;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parser.Syntax
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
