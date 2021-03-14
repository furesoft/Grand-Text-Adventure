﻿using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.Parsers.EntityParser.Syntax;
using GrandTextAdventure.Core.Parsing;
using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    public partial class EntityDefinitionParser : BaseParser<SyntaxNode>
    {
        public SyntaxNode ParseBlock()
        {
            var members = ParseMembers();

            return new BlockNode(members);
        }

        protected override void InitTokenizer()
        {
            Tokenizer.AddDefinition(SyntaxKind.EndToken, "end", 1);
            Tokenizer.AddDefinition(SyntaxKind.PropertyToken, "property", 1);
            Tokenizer.AddDefinition(SyntaxKind.EntityModelToken, "entitymodel", 1);
            Tokenizer.AddDefinition(SyntaxKind.EntityToken, "entity", 1);
            Tokenizer.AddDefinition(SyntaxKind.IsToken, "is", 1);
            Tokenizer.AddDefinition(SyntaxKind.ApplyModelToken, "appylmodel", 1);

            Tokenizer.AddDefinition(SyntaxKind.IntLiteralToken, "[0-9]+", 2, typeof(int));
            Tokenizer.AddDefinition(SyntaxKind.StringLiteralToken, "\".*?\"", 2);
            Tokenizer.AddDefinition(SyntaxKind.CommentToken, @"/\\*.*?\\*/", 1);
            Tokenizer.AddDefinition(SyntaxKind.IdentifierToken, "[a-zA-Z_][0-9a-zA-F_]*", 1);

            Tokenizer.AddDefinition(SyntaxKind.EqualsToken, "=", 1);
        }

        protected override SyntaxNode InternalParse()
        {
            var block = ParseBlock();

            MatchToken(SyntaxKind.EndOfFile);

            return block;
        }

        private static Token RemoveStringCharakter(Token token)
        {
            var content = token.Text.Substring(1, token.Length - 2);

            return new Token(token.Kind, content, token.Start, token.End, token.ReturnType);
        }

        private SyntaxNode ParseEntityDefinition()
        {
            throw new NotImplementedException();
        }

        private SyntaxNode ParseEntityModelDefinition()
        {
            var keywordToken = NextToken();
            var nameToken = MatchToken(SyntaxKind.StringLiteralToken);

            nameToken = RemoveStringCharakter(nameToken);

            var properties = new BlockNode(null);

            return new EntityModelDefinitionNode(keywordToken, nameToken, properties);
        }

        private SyntaxNode ParseMember()
        {
            SyntaxNode node;
            if (Current.Kind == SyntaxKind.EntityModelToken)
            {
                node = ParseEntityModelDefinition();
            }
            else if (Current.Kind == SyntaxKind.EntityToken)
            {
                node = ParseEntityDefinition();
            }
            else
            {
                Diagnostics.ReportUnexpectedDeclaration(Current.Span, Current);
                return null;
            }

            MatchToken(SyntaxKind.EndToken);

            return node;
        }

        private IEnumerable<SyntaxNode> ParseMembers()
        {
            var members = new List<SyntaxNode>();

            while (Current.Kind != SyntaxKind.EndOfFile)
            {
                var startToken = Current;
                var member = ParseMember();

                if (member is BlockNode bn)
                {
                    members.AddRange(bn.Children);
                }
                else
                {
                    if (member != null)
                    {
                        members.Add(member);
                    }
                }

                // If ParseMember() did not consume any tokens,
                // we need to skip the current token and continue
                // in order to avoid an infinite loop.
                //
                // We don't need to report an error, because we'll
                // already tried to parse an expression statement
                // and reported one.
                if (Current == startToken)
                    NextToken();
            }

            return members;
        }
    }
}
