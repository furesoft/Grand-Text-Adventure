using System;
using System.Collections.Generic;
using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Parser;
using GrandTextAdventure.Core.Parser.Syntax;

namespace EflCompiler
{
    internal class CompilerAstVisitor : IScriptVisitor
    {
        private GameObject _tempObject;
        private GameObjectWriter _writer;

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
