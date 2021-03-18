using System.Collections.Generic;
using GrandTextAdventure.Core.Parser.Syntax;
using GrandTextAdventure.Core.Parsing;

namespace GrandTextAdventure.Core.Parser
{
    public partial class EflDefinitionParser : BaseParser<SyntaxNode, SyntaxKind>
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
            Tokenizer.AddDefinition(SyntaxKind.ApplyModelToken, "applymodel", 1);

            Tokenizer.AddDefinition(SyntaxKind.IntLiteralToken, "[0-9]+", 2, typeof(int));
            Tokenizer.AddDefinition(SyntaxKind.StringLiteralToken, "\".*?\"", 2, typeof(StringTokenConverter));
            Tokenizer.AddDefinition(SyntaxKind.CommentToken, @"/\\*.*?\\*/", 1);
            Tokenizer.AddDefinition(SyntaxKind.IdentifierToken, "[a-zA-Z_][0-9a-zA-F_]*", 2);

            Tokenizer.AddDefinition(SyntaxKind.EqualsToken, "=", 1);
        }

        protected override SyntaxNode InternalParse()
        {
            var block = ParseBlock();

            MatchToken(SyntaxKind.EndOfFile);

            return block;
        }

        private SyntaxNode ParseApplyModel()
        {
            var keywordToken = MatchToken(SyntaxKind.ApplyModelToken);
            var nameToken = MatchToken(SyntaxKind.StringLiteralToken);

            return new ApplyModelDefinition(keywordToken, nameToken);
        }

        private SyntaxNode ParseEntityDefinition()
        {
            var keywordToken = NextToken();
            var nameToken = MatchToken(SyntaxKind.StringLiteralToken);

            var isKeyword = MatchToken(SyntaxKind.IsToken);
            var typeId = MatchToken(SyntaxKind.IdentifierToken);

            var members = ParseEntityMembers();

            var endToken = MatchToken(SyntaxKind.EndToken);

            return new EntityDefinitionNode(keywordToken, nameToken, isKeyword, typeId, new BlockNode(members), endToken);
        }

        private SyntaxNode ParseEntityMember()
        {
            SyntaxNode node;
            if (Current.TokenKind<SyntaxKind>() == SyntaxKind.PropertyToken)
            {
                node = ParseProperty();
            }
            else if (Current.TokenKind<SyntaxKind>() == SyntaxKind.ApplyModelToken)
            {
                node = ParseApplyModel();
            }
            else
            {
                Diagnostics.ReportUnexpectedDeclaration(Current.Span, Current);
                return null;
            }

            return node;
        }

        private IEnumerable<SyntaxNode> ParseEntityMembers()
        {
            var members = new List<SyntaxNode>();

            while (Current.TokenKind<SyntaxKind>() != SyntaxKind.EndToken)
            {
                var startToken = Current;
                var member = ParseEntityMember();

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

        private SyntaxNode ParseEntityModelDefinition()
        {
            var keywordToken = NextToken();
            var nameToken = MatchToken(SyntaxKind.StringLiteralToken);

            var properties = ParseProperties();

            var endToken = MatchToken(SyntaxKind.EndToken);

            return new EntityModelDefinitionNode(keywordToken, nameToken, new BlockNode(properties), endToken);
        }

        private SyntaxNode ParseMember()
        {
            SyntaxNode node;
            if (Current.TokenKind<SyntaxKind>() == SyntaxKind.EntityModelToken)
            {
                node = ParseEntityModelDefinition();
            }
            else if (Current.TokenKind<SyntaxKind>() == SyntaxKind.EntityToken)
            {
                node = ParseEntityDefinition();
            }
            else
            {
                Diagnostics.ReportUnexpectedDeclaration(Current.Span, Current);
                return null;
            }

            return node;
        }

        private IEnumerable<SyntaxNode> ParseMembers()
        {
            var members = new List<SyntaxNode>();

            while (Current.TokenKind<SyntaxKind>() != SyntaxKind.EndOfFile)
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

        private IEnumerable<SyntaxNode> ParseProperties()
        {
            var properties = new List<SyntaxNode>();

            while (Current.TokenKind<SyntaxKind>() != SyntaxKind.EndToken)
            {
                var startToken = Current;
                var property = ParseProperty();

                if (property is BlockNode bn)
                {
                    properties.AddRange(bn.Children);
                }
                else
                {
                    if (property != null)
                    {
                        properties.Add(property);
                    }
                }

                // If we did not consume any tokens,
                // we need to skip the current token and continue
                // in order to avoid an infinite loop.
                if (Current == startToken)
                    NextToken();
            }

            return properties;
        }

        private SyntaxNode ParseProperty()
        {
            var keywordToken = MatchToken(SyntaxKind.PropertyToken);

            var identifierToken = MatchToken(SyntaxKind.IdentifierToken);
            var equalsToken = MatchToken(SyntaxKind.EqualsToken);

            var valueToken = MatchToken(SyntaxKind.IntLiteralToken); //ToDo: Replace with ParseLiteral Method

            return new PropertyDefinitionNode(keywordToken, identifierToken, equalsToken, valueToken);
        }
    }
}
