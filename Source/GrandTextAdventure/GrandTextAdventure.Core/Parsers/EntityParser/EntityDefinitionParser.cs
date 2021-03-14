using GrandTextAdventure.Core.Parsing;

namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    public partial class EntityDefinitionParser : BaseParser<SyntaxNode>
    {
        protected override void InitTokenizer()
        {
            Tokenizer.AddDefinition(SyntaxKind.EndToken, "end", 1);
            Tokenizer.AddDefinition(SyntaxKind.PropertyToken, "property", 1);
            Tokenizer.AddDefinition(SyntaxKind.EntityModelToken, "entitymodel", 1);
            Tokenizer.AddDefinition(SyntaxKind.EntityToken, "entity", 1);
            Tokenizer.AddDefinition(SyntaxKind.IsToken, "is", 1);
            Tokenizer.AddDefinition(SyntaxKind.ApplyModelToken, "appylmodel", 1);

            Tokenizer.AddDefinition(SyntaxKind.IntLiteralToken, "[0-9]+", 1);
            Tokenizer.AddDefinition(SyntaxKind.StringLiteralToken, @"'.*?'", 1);
            Tokenizer.AddDefinition(SyntaxKind.CommentToken, @"/\\*.*?\\*/", 1);
            Tokenizer.AddDefinition(SyntaxKind.IdentifierToken, "[a-zA-Z_][0-9a-zA-F_]*", 1);

            Tokenizer.AddDefinition(SyntaxKind.EqualsToken, "=", 1);
        }

        protected override SyntaxNode InternalParse()
        {
            MatchToken(SyntaxKind.EOF);

            return null;
        }
    }
}
