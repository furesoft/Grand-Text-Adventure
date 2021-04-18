using System.Collections.Generic;

namespace GrandTextAdventure.Core
{
    public class EntityModel
    {
        public string Name { get; set; }

        public Dictionary<string, object> Properties { get; set; } = new();
    }
}
