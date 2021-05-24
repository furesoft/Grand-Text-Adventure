using System;
using System.Collections.Immutable;
using System.Linq;
using GrandTextAdventure.Core.Parser;
using GrandTextAdventure.Core.Parsing.Diagnostics;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsing
{
    public abstract class BaseParser<ReturnType, TokenType>
        where TokenType : IComparable
    {
        protected readonly PrecedenceBasedRegexTokenizer Tokenizer = new();
        protected int _position;
        protected ImmutableArray<Token> _tokens;
        public Token Current => Peek(0);
        public DiagnosticBag Diagnostics { get; } = new();

        public Token MatchToken(TokenType kind)
        {
            if (Current.TokenKind<TokenType>().CompareTo(kind) == 0)
                return NextToken();

            if (Current.TokenKind<TokenType>().CompareTo(kind) != 0) Diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);

            return new Token(SyntaxKind.Invalid, null, 0, 0, null);
        }

        public Token NextToken()
        {
            var current = Current;
            _position++;

            if (Current.TokenKind<int>().CompareTo(0) == 0)
                return NextToken();

            return current;
        }

        public ReturnType Parse(string src)
        {
            InitTokenizer();

            _tokens = Tokenizer.Tokenize(src).Where(_ => ((int)_.Kind).CompareTo(0) != 0).ToImmutableArray();

            return InternalParse();
        }

        public Token Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
            {
                return _tokens[^1];
            }

            return _tokens[index];
        }

        protected abstract void InitTokenizer();

        protected abstract ReturnType InternalParse();
    }
}
