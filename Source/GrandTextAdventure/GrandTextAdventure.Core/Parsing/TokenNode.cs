using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTextAdventure.Core.Parsers.EntityParser;
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
