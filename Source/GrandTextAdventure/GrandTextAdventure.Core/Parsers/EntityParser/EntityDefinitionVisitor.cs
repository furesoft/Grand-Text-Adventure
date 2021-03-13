using System;
using GrandTextAdventure.Core.Parsers.EntityParser.Syntax;

namespace GrandTextAdventure.Core.Parsers.EntityParser
{
    public class EntityDefinitionVisitor : IScriptVisitor
    {
        public GameObject Result { get; private set; }

        public void Visit(BlockNode block)
        {
            foreach (var member in block)
            {
                Visit((EntityDefinitionNode)member);
            }
        }

        public void Visit(EntityDefinitionNode definitionNode)
        {
            throw new NotImplementedException();
        }
    }
}
