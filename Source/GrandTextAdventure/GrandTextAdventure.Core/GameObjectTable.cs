using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public static IEnumerable<T> GetDefinitionsOfType<T>()
                    where T : GameObject
        {
            return from obj in s_objects
                   where obj is T
                   select (T)obj.Value;
        }

        public static void Load(string directory)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory, "*.entity");

                foreach (var entityConfiguration in files)
                {
                    AddDefinition(File.ReadAllText(entityConfiguration));
                }
            }
        }

        public static void Load(Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames().Where(_ => _.EndsWith(".efl"));

            foreach (var ressource in resourceNames)
            {
                var ressourceStream = assembly.GetManifestResourceStream(ressource);

                var strReader = new StreamReader(ressourceStream);

                AddDefinition(strReader.ReadToEnd());

                strReader.Close();
            }
        }

        private static void AddDefinition(string entityConfiguration)
        {
            var entityParser = new EntityDefinitionParser();

            var entityDefinitionAST = entityParser.Parse(entityConfiguration);
            var entityDefinitionVisitor = new EntityDefinitionVisitor();

            entityDefinitionAST.Accept(entityDefinitionVisitor);

            var resultObject = entityDefinitionVisitor.Result;

            s_objects.Add(resultObject.Name, resultObject);
        }
    }
}
