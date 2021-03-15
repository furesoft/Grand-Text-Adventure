using GrandTextAdventure.Core.Parsing.Tokenizer;

namespace GrandTextAdventure.Core.Parser.Syntax
{
    public class EntityDefinitionNode : DeclarationNode
    {
        public EntityDefinitionNode(Token keywordToken, Token nameToken, Token isToken, Token typeToken, BlockNode properties, Token endToken)
            : base(endToken)
        {
            KeywordToken = keywordToken;
            NameToken = nameToken;
            IsToken = isToken;
            TypeToken = typeToken;
            Properties = properties;
        }

        public Token IsToken { get; }
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
