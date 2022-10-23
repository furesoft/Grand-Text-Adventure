using System.Collections.Generic;
using System.Linq;
using Darlek.Core.RuntimeLibrary;
using Darlek.Scheme;
using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core.Scripting;

[RuntimeType]
public static class EntityFunctions
{
    [RuntimeMethod("entity-model")]
    public static EntityModel CreateModel(List<object> entityProperties)
    {
        return new EntityModel() { Properties = new(entityProperties.Cast<KeyValuePair<string, object>>()) };
    }

    [RuntimeMethod("property")]
    public static KeyValuePair<string, object> Property(string name, object value)
    {
        return new(name, value);
    }

    [RuntimeMethod("vehicle")]
    public static GameObject Vehicle(string name, List<object> args)
    {
        return Entity<Vehicle>(name, args);
    }

    [RuntimeMethod("weapon")]
    public static GameObject Weapon(string name, List<object> args)
    {
        return Entity<Weapon>(name, args);
    }

    [RuntimeMethod("applymodel")]
    public static ApplyModelContainer ApplyModel(EntityModel model)
    {
        return new ApplyModelContainer { Model = model };
    }

    [RuntimeMethod("export-entities")]
    public static object ExportEntities(List<object> entities)
    {
        foreach (var entity in entities)
        {
            var go = (GameObject)entity;

            GameObjectTable.Add(go);
        }

        return None.Instance;
    }

    private static T Entity<T>(string name, List<object> args)
                where T : GameObject, new()
    {
        var go = new T();
        go.Name = name;

        foreach (var item in args)
        {
            if (item is KeyValuePair<string, object> prop)
            {
                if (go.Properties.ContainsKey(prop.Key))
                {
                    go.Properties[prop.Key] = prop.Value;
                }
                else
                {
                    go.Properties.Add(prop.Key, prop.Value);
                }
            }
            else if (item is ApplyModelContainer amc)
            {
                foreach (var mp in amc.Model.Properties)
                {
                    go.Properties.Add(mp.Key, mp.Value);
                }
            }
        }

        return go;
    }

    public class ApplyModelContainer
    {
        public EntityModel Model { get; set; }
    }
}