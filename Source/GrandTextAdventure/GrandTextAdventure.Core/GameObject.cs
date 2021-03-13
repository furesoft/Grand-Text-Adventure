using System.Collections.Generic;
using System.Linq;

namespace GrandTextAdventure.Core
{
    public class GameObject
    {
        public virtual string Name { get; set; }

        public Dictionary<string, object> Properties { get; set; } = new();

        public void Apply(GameObject parent)
        {
            Name = parent.Name;

            Properties = parent.Properties.ToDictionary(entry => entry.Key, entry => entry.Value); // Clone Properties Dictionary
        }
    }
}
