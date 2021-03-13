using System.Collections.Generic;
using System.IO;
using GrandTextAdventure.Core.Parsers.EntityParser;

namespace GrandTextAdventure.Core
{
    public static class GameObjectTable
    {
        private static readonly Dictionary<string, GameObject> s_objects = new();

        public static TObject CreateInstance<TObject>(string name)
            where TObject : GameObject, new()
        {
            if (s_objects.ContainsKey(name))
            {
                var instance = new TObject();

                instance.Apply(s_objects[name]);

                return instance;
            }
            else
            {
                throw new KeyNotFoundException($"'{name}' GameObject is not found");
            }
        }

        public static void Load(string directory)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory, "*.entity");

                foreach (var entityConfiguration in files)
                {
                    var entityParser = new EntityDefinitionParser();

                    var entityDefinitionAST = entityParser.Parse(File.ReadAllText(entityConfiguration));
                    var entityDefinitionVisitor = new EntityDefinitionVisitor();

                    entityDefinitionAST.Accept(entityDefinitionVisitor);

                    var resultObject = entityDefinitionVisitor.Result;

                    s_objects.Add(resultObject.Name, resultObject);
                }
            }
        }
    }
}
