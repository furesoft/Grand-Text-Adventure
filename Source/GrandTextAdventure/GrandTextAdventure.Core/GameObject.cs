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

        public virtual string Name { get; set; }

        public Dictionary<string, object> Properties { get; set; } = new();

        public void Apply(GameObject parent)
        {
            Name = parent.Name;

            Properties = parent.Properties.ToDictionary(entry => entry.Key, entry => entry.Value); // Clone Properties Dictionary
        }

        public T GetValue<T>(string name)
        {
            if (Properties.ContainsKey(name))
            {
                return (T)Properties[name];
            }

            return default;
        }

        public void SetOrAddValue(string name, object value)
        {
            if (Properties.ContainsKey(name))
            {
                Properties[name] = value;
            }
            else
            {
                Properties.Add(name, value);
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
