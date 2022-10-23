using System.Collections.Generic;

namespace GrandTextAdventure.Core;

public static class GameObjectDefinitionLoader
{
    private static readonly Dictionary<string, EntityModel> s_models = new();

    public static void AddModel(EntityModel model)
    {
        s_models.Add(model.Name, model);
    }

    public static EntityModel GetModel(string name)
    {
        if (s_models.ContainsKey(name))
        {
            return s_models[name];
        }

        return null;
    }

    public static IEnumerable<GameObject> LoadDefinitions(string entityConfiguration)
    {
        //ToDo: implement loading entitiy definitions from script file
        return null;
    }
}
