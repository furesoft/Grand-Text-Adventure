using System;
using GrandTextAdventure.Core.Parser.Syntax;

namespace GrandTextAdventure.Core.Parser
{
    public class EntityDefinitionVisitor : IScriptVisitor
    {
        public GameObject[] Result { get; private set; }

        public void Visit(BlockNode block)
        {
            foreach (var member in block)
            {
                if (member is EntityDefinitionNode defNode)
                {
                    Visit(defNode);
                }
                else if (member is EntityModelDefinitionNode modelDef)
                {
                    Visit(modelDef);
                }
            }
        }

        public void Visit(EntityDefinitionNode definitionNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(EntityModelDefinitionNode modelDefinitionNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(PropertyDefinitionNode definitionNode)
        {
            throw new NotImplementedException();
        }
    }
}
