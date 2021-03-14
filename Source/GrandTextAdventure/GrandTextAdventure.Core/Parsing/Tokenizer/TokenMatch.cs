using System;
using GrandTextAdventure.Core.Parsers.EntityParser;

namespace GrandTextAdventure.Core.Parsing.Tokenizer
{
    public class TokenMatch
    {
        public int EndIndex { get; set; }
        public int Precedence { get; set; }
        public int StartIndex { get; set; }
        public SyntaxKind TokenType { get; set; }
        public string Value { get; set; }
        public Type ReturnType { get; internal set; }
    }
}
