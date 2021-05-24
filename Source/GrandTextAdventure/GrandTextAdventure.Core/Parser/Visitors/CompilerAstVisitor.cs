using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.Parser.Syntax;

namespace GrandTextAdventure.Core.Parser.Visitors
{
    public class CompilerAstVisitor : IScriptVisitor
    {
        private readonly GameObjectWriter _writer;
        private GameObject _tempObject;

        public CompilerAstVisitor(GameObjectWriter writer)
        {
            _writer = writer;

            GameObjectTable.Init();
        }

        public void Visit(BlockNode block)
        {
            foreach (var child in block.Children)
            {
                if (child is EntityDefinitionNode edn)
                {
                    Visit(edn);
                }
                else if (child is EntityModelDefinitionNode emn)
                {
                    Visit(emn);
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

            _writer.WriteObject(_tempObject);
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
