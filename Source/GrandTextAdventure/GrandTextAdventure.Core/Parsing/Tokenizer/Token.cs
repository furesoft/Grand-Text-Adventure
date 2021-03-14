using System;
using System.ComponentModel;
using GrandTextAdventure.Core.Parsers.EntityParser;
using GrandTextAdventure.Core.Parsing.Text;

namespace GrandTextAdventure.Core.Parsing.Tokenizer
{
    public sealed class Token
    {
        private readonly Type _returnType;

        public Token(SyntaxKind kind, string text, int start, int end, Type returnType)
        {
            Kind = kind;
            Text = text;
            Start = start;
            End = end;
            _returnType = returnType;
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

        public object Value
        {
            get
            {
                if (_returnType != null)
                {
                    var typeConverter = TypeDescriptor.GetConverter(_returnType);

                    return typeConverter.ConvertFromString(Text);
                }

                return Text;
            }
        }

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
