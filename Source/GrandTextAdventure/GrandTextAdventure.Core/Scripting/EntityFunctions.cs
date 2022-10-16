using System.Collections.Generic;
using System.Linq;
using Darlek.Core.RuntimeLibrary;

namespace GrandTextAdventure.Core.Scripting
{
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
            var entity = Entity(name, args);
            entity.Type = GameObjectType.Vehicle;

            return entity;
        }

        [RuntimeMethod("weapon")]
        public static GameObject Weapon(string name, List<object> args)
        {
            var entity = Entity(name, args);
            entity.Type = GameObjectType.Weapon;

            return entity;
        }

        [RuntimeMethod("entity")]
        public static GameObject Entity(string name, List<object> args)
        {
            var go = new GameObject();
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

        [RuntimeMethod("applymodel")]
        public static ApplyModelContainer ApplyModel(EntityModel model)
        {
            return new ApplyModelContainer { Model = model };
        }

        public class ApplyModelContainer
        {
            public EntityModel Model { get; set; }
        }
    }
}
