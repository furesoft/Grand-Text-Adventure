using System;
using System.ComponentModel;
using GrandTextAdventure.Core.Parsers.EntityParser;
using GrandTextAdventure.Core.Parsing.Text;

namespace GrandTextAdventure.Core.Parsing.Tokenizer
{
    public sealed class Token
    {
        public Token(SyntaxKind kind, string text, int start, int end, Type returnType)
        {
            Kind = kind;
            Text = text;
            Start = start;
            End = end;
            ReturnType = returnType;
            Length = end - start;
        }

        public static Token EndOfFile
        {
            get { return new Token(SyntaxKind.EndOfFile, "", 0, 0, null); }
        }

        public int End { get; }
        public SyntaxKind Kind { get; private set; }
        public int Length { get; set; }
        public Type ReturnType { get; }

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
                if (ReturnType != null)
                {
                    var typeConverter = TypeDescriptor.GetConverter(ReturnType);

                    return typeConverter.ConvertFromString(Text);
                }

                return Text;
            }
        }

        public override string ToString()
        {
            return "(" + Kind + ", \"" + Text + "\")";
        }
    }
}
