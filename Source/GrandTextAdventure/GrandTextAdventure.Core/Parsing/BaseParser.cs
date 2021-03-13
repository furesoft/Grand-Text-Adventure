using System.Collections.Immutable;
using GrandTextAdventure.Core.Parsers.EntityParser;
using GrandTextAdventure.Core.Parsing.Diagnostics;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsing
{
    public abstract class BaseParser<ReturnType>
    {
        protected int _position;
        protected ImmutableArray<Token> _tokens;
        private readonly PrecedenceBasedRegexTokenizer _tokenizer = new();
        public Token Current => Peek(0);
        public DiagnosticBag Diagnostics { get; } = new();

        public Token MatchToken(SyntaxKind kind)
        {
            if (Current.Kind.CompareTo(kind) == 0)
                return NextToken();

            if (Current.Kind.CompareTo(kind) != 0) Diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);

            return new Token(kind, null);
        }

        public Token NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        public ReturnType Parse(string src)
        {
            _tokens = _tokenizer.Tokenize(src).ToImmutableArray();

            return InternalParse();
        }

        public Token Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[^1];

            return _tokens[index];
        }

        protected abstract ReturnType InternalParse();
    }
}
