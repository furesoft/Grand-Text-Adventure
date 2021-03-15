using System;
using System.Collections.Generic;
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
            var model = new EntityModel
            {
                Name = modelDefinitionNode.NameToken.Text,
                Properties = ConvertToDictionary(modelDefinitionNode.Properties)
            };

            GameObjectDefinitionLoader.AddModel(model);
        }

        public void Visit(PropertyDefinitionNode definitionNode)
        {
            throw new NotImplementedException();
        }

        public void Visit(ApplyModelDefinition applyModelDefinition)
        {
            throw new NotImplementedException();
        }

        private static Dictionary<string, object> ConvertToDictionary(BlockNode properties)
        {
            var res = new Dictionary<string, object>();

            foreach (PropertyDefinitionNode item in properties)
            {
                res.Add(item.NameToken.Text, item.Value.Value);
            }

            return res;
        }
    }
}
