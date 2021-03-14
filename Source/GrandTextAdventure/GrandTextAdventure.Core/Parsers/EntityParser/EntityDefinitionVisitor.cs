using System;
using GrandTextAdventure.Core.Parsers.EntityParser.Syntax;

namespace GrandTextAdventure.Core.Parsers.EntityParser
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
    }
}
