using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.Parser.Syntax;

namespace GrandTextAdventure.Core.Parser.Visitors
{
    public class EntityDefinitionVisitor : IScriptVisitor
    {
        private GameObject _tempObject;

        public EntityDefinitionVisitor()
        {
            GameObjectTable.Init();
        }

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
            _tempObject = GameObjectTable.GetEntity(definitionNode.TypeToken.Text);

            if (_tempObject == null)
            {
                return;
            }

            foreach (var item in definitionNode.Children)
            {
                if (item is PropertyDefinitionNode propDef)
                {
                    _tempObject.Properties.AddOrUpdate(propDef.NameToken.Text, propDef.Value.Value);
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
                        _tempObject.Properties.AddOrUpdate(prop.Key, prop.Value);
                    }
                }
            }

            _tempObject.Name = definitionNode.NameToken.Value.ToString();

            Result.Add(_tempObject);
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

        public void Visit(LiteralNode literalNode)
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
