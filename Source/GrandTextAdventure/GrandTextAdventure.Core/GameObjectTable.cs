using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GrandTextAdventure.Core
{
    public static class GameObjectTable
    {
        private static readonly Dictionary<string, Type> s_instanceBuilders = new();
        private static readonly Dictionary<string, GameObject> s_objects = new();
        private static int s_idCounter = 1;

        public static void Add(GameObject obj)
        {
            s_objects.Add(obj.Name, obj);
        }

        public static TObject CreateInstance<TObject>(string name)
                    where TObject : GameObject, new()
        {
            if (s_objects.ContainsKey(name))
            {
                var instance = new TObject();

                instance.Apply(s_objects[name]);

                if (instance.ID == 0)
                {
                    instance.ID = s_idCounter++;
                }

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

        public static IEnumerable<GameObject> GetAll()
        {
            return s_objects.Values;
        }

        public static GameObject GetEntity(string name)
        {
            if (s_instanceBuilders.ContainsKey(name))
            {
                return (GameObject)Activator.CreateInstance(s_instanceBuilders[name]);
            }

            return null;
        }

        public static void Init()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(_ => typeof(GameObject).IsAssignableFrom(_));

            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<EntityInstance>();

                if (attr != null)
                {
                    s_instanceBuilders.Add(type.Name.ToLower(), type);
                }
            }
        }

        public static void Remove(string name)
        {
            s_objects.Remove(name);
        }

        public static void Load(string directory)
        {
            if (Directory.Exists(directory))
            {
                var files = Directory.GetFiles(directory, "*.efl");

                foreach (var entityConfiguration in files)
                {
                    AddDefinitions(File.ReadAllText(entityConfiguration));
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

                AddDefinitions(strReader.ReadToEnd());

                strReader.Close();
            }
        }

        private static void AddDefinitions(string entityConfiguration)
        {
            var resultObjects = GameObjectDefinitionLoader.LoadDefinitions(entityConfiguration);

            foreach (var obj in resultObjects)
            {
                s_objects.Add(obj.Name, obj);
            }
        }
    }
}
