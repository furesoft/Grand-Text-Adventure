using System.IO;
using System.Reflection;
using Darlek.Core.RuntimeLibrary;
using static Darlek.Scheme.Interpreter;

namespace GrandTextAdventure.Core;

public class Script
{
    public readonly Darlek.Scheme.Interpreter interpreter = new();

    public Script()
    {
        SchemeCliLoader.Apply(Assembly.GetEntryAssembly(), interpreter);
        SchemeCliLoader.Apply(Assembly.GetExecutingAssembly(), interpreter);
        SchemeCliLoader.Apply(Assembly.GetCallingAssembly(), interpreter);
        SchemeCliLoader.Apply(typeof(Darlek.Library.BinaryMethods).Assembly, interpreter);
    }

    public string Source { get; set; }

    public EvaluationResult Execute()
    {
        return interpreter.Evaluate(new StringReader(Source));
    }
}
