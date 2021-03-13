using GrandTextAdventure.Core.Parsing;

namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    public partial class EntityDefinitionParser : BaseParser<SyntaxNode>
    {
        protected override SyntaxNode InternalParse()
        {
            MatchToken(SyntaxKind.EOF);

            return null;
        }
    }
}
