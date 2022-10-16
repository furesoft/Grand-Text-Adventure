using System.IO;
using System.Reflection;
using Darlek.Core.RuntimeLibrary;

namespace GrandTextAdventure.Core
{
    public class Script
    {
        public Darlek.Scheme.Interpreter interpreter = new();

        public Script()
        {
            SchemeCliLoader.Apply(Assembly.GetEntryAssembly(), interpreter);
        }

        public string Source { get; set; }

        public object Execute()
        {
            return interpreter.Evaluate(new StringReader(Source));
        }
    }
}
