using GrandTextAdventure.Core.Parsing;

namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    public partial class EntityDefinitionParser : BaseParser<SyntaxNode>
    {
        protected override void InitTokenizer()
        {
            _tokenizer.AddDefinition(SyntaxKind.EndToken, "end", 1);
            _tokenizer.AddDefinition(SyntaxKind.PropertyToken, "property", 1);
            _tokenizer.AddDefinition(SyntaxKind.EntityModelToken, "entitymodel", 1);
            _tokenizer.AddDefinition(SyntaxKind.EntityToken, "entity", 1);
            _tokenizer.AddDefinition(SyntaxKind.IsToken, "is", 1);
            _tokenizer.AddDefinition(SyntaxKind.ApplyModelToken, "appylmodel", 1);

            _tokenizer.AddDefinition(SyntaxKind.IntLiteralToken, "[0-9]+", 1);
            _tokenizer.AddDefinition(SyntaxKind.StringLiteralToken, @"'.*?'", 1);
            _tokenizer.AddDefinition(SyntaxKind.CommentToken, @"/\\*.*?\\*/", 1);
            _tokenizer.AddDefinition(SyntaxKind.IdentifierToken, "[a-zA-Z_][0-9a-zA-F_]*", 1);

            _tokenizer.AddDefinition(SyntaxKind.EqualsToken, "=", 1);
        }

        protected override SyntaxNode InternalParse()
        {
            MatchToken(SyntaxKind.EOF);

            return null;
        }
    }
}
