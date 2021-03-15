using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.Entities;
using GrandTextAdventure.Core.Parser.Syntax;

namespace GrandTextAdventure.Core.Parser
{
    public class EntityDefinitionVisitor : IScriptVisitor
    {
        private GameObject tempObject;
        public List<GameObject> Result { get; private set; } = new();

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
            if (definitionNode.TypeToken.Text == "vehicle")
            {
                tempObject = new Vehicle();
            }

            foreach (var item in definitionNode.Children)
            {
                if (item is PropertyDefinitionNode propDef)
                {
                    tempObject.Properties.Add(propDef.NameToken.Text, propDef.Value.Value);
                }
                else if (item is ApplyModelDefinition applyModelDef)
                {
                    var model = GameObjectDefinitionLoader.GetModel(applyModelDef.NameToken.Value.ToString());

                    if (model == null)
                    {
                        return;
                    }

                    foreach (var prop in model.Properties)
                    {
                        tempObject.Properties.Add(prop.Key, prop.Value);
                    }
                }
            }

            tempObject.Name = definitionNode.NameToken.Value.ToString();

            Result.Add(tempObject);
        }

        public void Visit(EntityModelDefinitionNode modelDefinitionNode)
        {
            var model = new EntityModel
            {
                Name = modelDefinitionNode.NameToken.Value.ToString(),
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
