using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsers.EntityParser.Syntax
{
    public class EntityModelDefinitionNode : DeclarationNode
    {
        public EntityModelDefinitionNode(Token keywordToken, Token nameToken, BlockNode properties, Token endToken)
            : base(endToken)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
            Properties = properties;
        }

        public Token KeywordToken { get; }
        public Token NameToken { get; }
        public BlockNode Properties { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
