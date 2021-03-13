using GrandTextAdventure.Core.Parsers.EntityParser;
using GrandTextAdventure.Core.Parsing.Text;

namespace GrandTextAdventure.Core.Parsing.Tokenizer
{
    public sealed class Token
    {
        public Token(SyntaxKind kind, string text, int start, int end)
        {
            Kind = kind;
            Text = text;
            Start = start;
            End = end;
            Length = end - start;
        }

        public Token(SyntaxKind kind, string text)
        {
            Kind = kind;
            Text = text;
            Length = text.Length;
        }

        public static Token EndOfFile
        {
            get { return new Token(SyntaxKind.EndOfFile, ""); }
        }

        public int End { get; }
        public SyntaxKind Kind { get; private set; }
        public int Length { get; set; }

        public TextSpan Span
        {
            get
            {
                return new TextSpan(Start, Length);
            }
        }

        public int Start { get; }
        public string Text { get; private set; }

        public static bool IsTrivia(SyntaxKind kind)
        {
            return kind == SyntaxKind.Whitespace
                || kind == SyntaxKind.Comment;
        }

        public override string ToString()
        {
            return "(" + Kind + ", \"" + Text + "\")";
        }
    }
}
