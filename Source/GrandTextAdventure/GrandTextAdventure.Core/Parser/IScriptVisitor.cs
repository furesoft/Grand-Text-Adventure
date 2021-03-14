using GrandTextAdventure.Core.Parser.Syntax;

namespace GrandTextAdventure.Core.Parser
{
    public interface IScriptVisitor
    {
        void Visit(BlockNode block);

        void Visit(EntityDefinitionNode definitionNode);

        void Visit(EntityModelDefinitionNode definitionNode);

        void Visit(PropertyDefinitionNode definitionNode);
    }
}
