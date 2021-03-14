using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrandTextAdventure.Core.Parsers.EntityParser;

namespace GrandTextAdventure.Core
{
    public static class GameObjectDefinitionLoader
    {
        private static readonly Dictionary<string, EntityModel> s_models = new();

        public static void AddModel(EntityModel model)
        {
            s_models.Add(model.Name, model);
        }

        public static IEnumerable<GameObject> LoadDefinitions(string entityConfiguration)
        {
            var entityParser = new DefinitionParser();

            var entityDefinitionAST = entityParser.Parse(entityConfiguration);
            var entityDefinitionVisitor = new EntityDefinitionVisitor();

            entityDefinitionAST.Accept(entityDefinitionVisitor);

            return entityDefinitionVisitor.Result;
        }
    }
}
