using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace GrandTextAdventure.Core
{
    public class GameObject : DynamicObject
    {
        public int ID
        {
            get { return GetValue<int>("ID"); }
            set { SetOrAddValue("ID", value); }
        }

        public string Name { get => GetValue<string>(nameof(Name)); set => SetOrAddValue(nameof(Name), value); }

        public Dictionary<string, object> Properties { get; set; } = new();
        public GameObjectType Type { get; internal set; }

        public void Apply(GameObject parent)
        {
            Name = parent.Name;

            Properties = parent.Properties.ToDictionary(entry => entry.Key, entry => entry.Value); // Clone Properties Dictionary
        }

        public T GetValue<T>(string name)
        {
            var lowerName = name.ToLower();

            if (Properties.ContainsKey(lowerName))
            {
                return (T)Properties[lowerName];
            }

            return default;
        }

        public void SetOrAddValue(string name, object value)
        {
            if (Properties.ContainsKey(name.ToLower()))
            {
                Properties[name.ToLower()] = value;
            }
            else
            {
                Properties.Add(name.ToLower(), value);
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetValue<object>(binder.Name);

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SetOrAddValue(binder.Name, value);

            return true;
        }
    }
}
