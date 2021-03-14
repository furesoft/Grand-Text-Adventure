using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parsers.EntityParser.Syntax
{
    public class EntityDefinitionNode : DeclarationNode
    {
        public EntityDefinitionNode(Token keywordToken, Token nameToken, Token typeToken, BlockNode properties, Token endToken)
            : base(endToken)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
            TypeToken = typeToken;
            Properties = properties;
        }

        public Token KeywordToken { get; }
        public Token NameToken { get; }
        public BlockNode Properties { get; }
        public Token TypeToken { get; }

        public override void Accept(IScriptVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
